namespace SurveyMaker.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Admin;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminUserService users;

        public UsersController(
            UserManager<User> userManager,
            IAdminUserService users)
        {
            this.userManager = userManager;
            this.users = users;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();

            return View(new UserListingsViewModel
            {
                Users = users
            });
        }
    }
}