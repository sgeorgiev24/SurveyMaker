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

    [Authorize]
    public class PollsController : Controller
    {
        // TODO: Delete Poll

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
        public IActionResult Create(CreatePollFormViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            this.polls.Create(model.Name, model.Description, userId);
            
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
        public IActionResult Details(int id)
        {
            if (!this.polls.PollExist(id))
            {
                return NotFound();
            }
            var model = this.polls.GetPollDetails(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.polls.PollExist(id))
            {
                return NotFound();
            }

            var model = this.polls.PollById(id);
            
            return View(model);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(int id, PollFormServiceModel model)
        {
            this.polls.Edit(id, model.Name, model.Description);

            return RedirectToAction(nameof(All));
        }
    }
}