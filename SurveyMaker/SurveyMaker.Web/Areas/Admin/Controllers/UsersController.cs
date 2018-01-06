namespace SurveyMaker.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Admin;
    using SurveyMaker.Web.Controllers;
    using SurveyMaker.Web.Infrastructure.Extensions;
    using System.Threading.Tasks;

    using static WebConstants;

    public class UsersController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminUserService users;
        private readonly SignInManager<User> signInManager;

        public UsersController(
            UserManager<User> userManager,
            IAdminUserService users,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.users = users;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            var userIsAdmin = await userManager.IsInRoleAsync(currentUser, AdministratorRole);

            if (!userIsAdmin)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
            }

            return View(new UserListingsViewModel
            {
                Users = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin([FromForm]string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid user");
            }

            await this.userManager.AddToRoleAsync(user, AdministratorRole);

            TempData.AddSuccessMessage($"User \"{user.UserName}\" is set as admin.");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin([FromForm]string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid user");
            }

            await this.userManager.RemoveFromRoleAsync(user, AdministratorRole);

            TempData.AddSuccessMessage($"User \"{user.UserName}\" is removed from admin group.");

            if (currentUser == user)
            {
                await this.signInManager.SignOutAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}