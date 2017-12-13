namespace SurveyMaker.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.PollViewModels;
    using Services;
    using System.Threading.Tasks;

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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreatePollFormModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.polls.CreateAsync(model.Name, model.Description, userId);
            var pollId = this.polls.CreateAsync(model.Name, model.Description, userId).Id;

            // TODO: Redirect to Polls/All
            return RedirectToAction(nameof(All), new {  });
        }

        [HttpGet]
        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(User);
            var model = this.polls.PollByUserId(userId);

            return View(model);
        }
    }
}