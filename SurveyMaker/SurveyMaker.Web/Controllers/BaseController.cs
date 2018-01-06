namespace SurveyMaker.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BaseController : Controller
    {
        private readonly UserManager<User> userManager;

        public BaseController()
        {

        }

        public BaseController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> CheckUser(string authorId)
        {
            var currentUser = await this.userManager.GetUserAsync(HttpContext.User);
            var haveAccess = currentUser.Id == authorId;
            var isAdmin = await this.userManager.IsInRoleAsync(currentUser, WebConstants.AdministratorRole);

            if (!haveAccess && !isAdmin)
            {
                return true;
            }
            return false;
        }
    }
}