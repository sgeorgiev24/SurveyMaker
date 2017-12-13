namespace SurveyMaker.Services.Implementations
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using System;
    using SurveyMaker.Services.Models;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;

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

            this.db.Add(poll);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<PollListingServiceModel> PollByUserId(string userId)
            => this.db.Polls
                .Where(p => p.AuthorId == userId)
                .Select(p => new PollListingServiceModel
                {
                    Name = p.Name,
                    Author = p.Author.UserName,
                    AuthorId = p.AuthorId,
                    Description = p.Description,
                    Id = p.Id,
                    Questions = p.Questions.Count,
                    UsersCompleted = p.UsersCompleted
                })
                //.ProjectTo<PollListingServiceModel>()
                .ToList();
    }
}
