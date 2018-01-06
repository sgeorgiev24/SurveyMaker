namespace SurveyMaker.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using System.Threading.Tasks;
    using SurveyMaker.Services.Admin.Models.Poll;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class AdminPollService : IAdminPollService
    {
        private readonly SurveyMakerDbContext db;

        public AdminPollService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<PollListingsServiceModel>> AllAsync()
            => await this.db.Polls
                .ProjectTo<PollListingsServiceModel>()
                .OrderByDescending(p => p.Id)
                .ToListAsync();
    }
}
