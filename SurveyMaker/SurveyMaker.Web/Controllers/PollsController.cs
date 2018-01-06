namespace SurveyMaker.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.PollViewModels;
    using Services;
    using Services.Models.Poll;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    [Authorize]
    public class PollsController : Controller
    {
        private readonly IPollService polls;
        private readonly UserManager<User> userManager;
        private readonly IUserService users;

        public PollsController(
            IPollService polls,
            UserManager<User> userManager,
            IUserService users)
        {
            this.polls = polls;
            this.userManager = userManager;
            this.users = users;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreatePollFormViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.polls.CreateAsync(model.Name, model.Description, userId);

            TempData.AddSuccessMessage($"Survey \"{model.Name}\" created.");

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var model = await this.polls.PollsByUserIdAsync(user.Id);
                
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!await this.polls.PollExistAsync(id))
            {
                return NotFound();
            }

            var model = await this.polls.GetPollDetailsAsync(id);

            if (!await this.users.HaveAccessAsync(model.AuthorId))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var currentUser = await this.userManager.GetUserAsync(HttpContext.User);

            if (!await this.polls.PollExistAsync(id))
            {
                return NotFound();
            }

            await this.polls.DeleteAsync(id);

            TempData.AddSuccessMessage("Survey deleted.");

            if (await this.userManager.IsInRoleAsync(currentUser, WebConstants.AdministratorRole))
            {
                return RedirectToAction("Index", "Polls", new { area = "Admin"});
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {            
            if (!await this.polls.PollExistAsync(id))
            {
                return NotFound();
            }
            
            var model = await this.polls.PollByIdAsync(id);

            if (!await this.users.HaveAccessAsync(model.AuthorId))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, PollFormServiceModel model)
        {
            await this.polls.EditAsync(id, model.Name, model.Description);

            TempData.AddSuccessMessage("Survey edited.");

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Complete(string urlToken)
        {
            var model = await this.polls.PollByUrlTokenAsync(urlToken);

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Complete(int pollId, Dictionary<string, string> formData)
        {
            await this.polls.SaveDataFromPollAsync(pollId, formData);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}