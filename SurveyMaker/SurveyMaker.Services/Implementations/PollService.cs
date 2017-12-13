namespace SurveyMaker.Services.Implementations
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using System;

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
    }
}
