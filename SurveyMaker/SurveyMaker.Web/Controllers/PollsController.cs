namespace SurveyMaker.Web.Controllers
{
    using Data.Models;
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
        public async Task<IActionResult> Create(CreatePollFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var userId = this.userManager.GetUserId(User);

            await this.polls.CreateAsync(model.Name, model.Description, userId);

            // TODO: Redirect to Polls/Edit/{id}
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}