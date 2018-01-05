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

        public PollsController(
            IPollService polls,
            UserManager<User> userManager)
        {
            this.polls = polls;
            this.userManager = userManager;
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
        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(User);
            var model = this.polls.PollsByUserId(userId);

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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.polls.PollExistAsync(id))
            {
                return NotFound();
            }

            await this.polls.DeleteAsync(id);

            TempData.AddSuccessMessage("Survey deleted.");

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