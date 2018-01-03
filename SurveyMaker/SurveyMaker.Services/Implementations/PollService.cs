namespace SurveyMaker.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Services.Models.Poll;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PollService : IPollService
    {
        private readonly SurveyMakerDbContext db;

        public PollService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string description, string authorId)
        {
            var poll = new Poll
            {
                Name = name,
                Description = description,
                AuthorId = authorId
            };

            poll.UrlToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            this.db.Add(poll);
            this.db.SaveChanges();
        }

        public void Edit(int pollId, string name, string description)
        {
            var poll = db.Polls.Find(pollId);

            poll.Name = name;
            poll.Description = description;

            db.SaveChanges();
        }

        public IEnumerable<PollListingServiceModel> PollsByUserId(string userId)
            => this.db.Polls
                .OrderByDescending(p => p.Id)
                .Where(p => p.AuthorId == userId)
                .ProjectTo<PollListingServiceModel>()
                .ToList();

        public bool PollExist(int id)
            => this.db.Polls.Any(p => p.Id == id);

        public PollFormServiceModel PollById(int id)
            => this.db.Polls
                .Where(p => p.Id == id)
                .ProjectTo<PollFormServiceModel>()
                .FirstOrDefault();

        public PollDetailsServiceModel GetPollDetails(int id)
            => this.db.Polls
                .Where(p => p.Id == id)
                .ProjectTo<PollDetailsServiceModel>()
                .FirstOrDefault();

        public PollCompleteServiceModel PollByUrlToken(string urlToken)
            => this.db.Polls
                .Where(p => p.UrlToken == urlToken)
                .ProjectTo<PollCompleteServiceModel>()
                .FirstOrDefault();

        public void SaveDataFromPoll(int pollId, Dictionary<string, string> formData)
        {
            var answersIds = new List<int>();

            var poll = this.db
                .Polls
                .Find(pollId);

            var pollQuestions = this.db
                .Questions
                .Where(q => q.PollId == poll.Id)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Title = q.Title
                })
                .ToList();

            foreach (var data in formData)
            {
                var keyIsQuestion = pollQuestions.Any(q => q.Title + "-" + q.Id == data.Key);
                if (keyIsQuestion)
                {
                    answersIds.Add(int.Parse(data.Value));
                }
            }

            poll.UsersCompleted++;
            foreach (var id in answersIds)
            {
                var answerOption = this.db.AnswerOptions.Find(id);
                answerOption.Votes++;
            }
            this.db.SaveChanges();
        }
    }
}
