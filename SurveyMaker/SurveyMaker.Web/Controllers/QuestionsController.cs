namespace SurveyMaker.Web.Controllers
{
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.QuestionViewModels;
    using Services;

    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService questions;

        public QuestionsController(IQuestionService questions)
        {
            this.questions = questions;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create([FromQuery]int pollId, CreateQuestionFormModel model)
        {
            this.questions.Create(pollId, model.Title, model.AnswerOptions);

            return RedirectToAction(nameof(PollsController.Edit), "Polls", new { id = pollId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.questions.QuestionExist(id))
            {
                return NotFound();
            }

            // TODO: get model and return View with model.
            return View();
        }
    }
}