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
    }
}
