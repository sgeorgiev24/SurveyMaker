namespace SurveyMaker.Services.Implementations
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Data.Models;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> HaveAccessAsync(string authorId)
        {
            var currentUser = await this.userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var haveAccess = currentUser.Id == authorId;
            var isAdmin = await this.userManager.IsInRoleAsync(currentUser, "Administrator");

            if (haveAccess || isAdmin)
            {
                return true;
            }
            return false;
        }
    }
}
