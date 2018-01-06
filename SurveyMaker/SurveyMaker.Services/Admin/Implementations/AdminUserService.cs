namespace SurveyMaker.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using SurveyMaker.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class AdminUserService : IAdminUserService
    {
        private readonly SurveyMakerDbContext db;
        private readonly UserManager<User> userManager;

        public AdminUserService(
            SurveyMakerDbContext db,
            UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AdminUserListingsServiceModel>> AllAsync()
        {
            var model = await this.db
                .Users
                .ProjectTo<AdminUserListingsServiceModel>()
                .ToListAsync();

            foreach (var user in model)
            {
                var currentUser = await userManager.FindByNameAsync(user.Username);
                var currentRole = await userManager.GetRolesAsync(currentUser);
                user.Role = currentRole;
            }

            return model;
        }
    }
}
