namespace SurveyMaker.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Services.Models.Poll;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PollService : IPollService
    {
        private readonly SurveyMakerDbContext db;

        public PollService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name, string description, string authorId)
        {
            var poll = new Poll
            {
                Name = name,
                Description = description,
                AuthorId = authorId
            };

            poll.UrlToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            await this.db.AddAsync(poll);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var poll = await this.db.Polls.FindAsync(id);

            this.db.Remove(poll);
            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(int pollId, string name, string description)
        {
            var poll = await db.Polls.FindAsync(pollId);

            poll.Name = name;
            poll.Description = description;

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PollListingServiceModel>> PollsByUserIdAsync(string userId, string search)
        {
            search = search ?? string.Empty;

            return await this.db.Polls
                .OrderByDescending(p => p.Id)
                .Where(p => p.AuthorId == userId && p.Name.ToLower().Contains(search.ToLower()))
                .ProjectTo<PollListingServiceModel>()
                .ToListAsync();
        }

        public async Task<bool> PollExistAsync(int id)
            => await this.db.Polls.AnyAsync(p => p.Id == id);

        public async Task<PollFormServiceModel> PollByIdAsync(int id)
            => await this.db.Polls
                .Where(p => p.Id == id)
                .ProjectTo<PollFormServiceModel>()
                .SingleOrDefaultAsync();

        public async Task<PollDetailsServiceModel> GetPollDetailsAsync(int id)
            => await this.db.Polls
                .Where(p => p.Id == id)
                .ProjectTo<PollDetailsServiceModel>()
                .SingleOrDefaultAsync();

        public async Task<PollCompleteServiceModel> PollByUrlTokenAsync(string urlToken)
            => await this.db.Polls
                .Where(p => p.UrlToken == urlToken)
                .ProjectTo<PollCompleteServiceModel>()
                .SingleOrDefaultAsync();

        public async Task SaveDataFromPollAsync(int pollId, Dictionary<string, string> formData)
        {
            var answersIds = new List<int>();

            var poll = await this.db
                .Polls
                .FindAsync(pollId);

            var pollQuestions = await this.db
                .Questions
                .Where(q => q.PollId == poll.Id)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Title = q.Title
                })
                .ToListAsync();

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
                var answerOption = await this.db.AnswerOptions.FindAsync(id);
                answerOption.Votes++;
            }
            await this.db.SaveChangesAsync();
        }
    }
}
