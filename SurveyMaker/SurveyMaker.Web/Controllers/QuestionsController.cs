namespace SurveyMaker.Web.Controllers
{
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.QuestionViewModels;
    using Services;
    using Services.Models.Question;
    using System.Collections.Generic;

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
        public IActionResult Create([FromQuery]int pollId, CreateQuestionFormViewModel model)
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

            var model = this.questions.QuestionById(id);

            return View(model);
        }
        
        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(int id, EditQuestionServiceModel model)
        {
            this.questions.Edit(id, model.Title, model.AnswerOptions, model.AnswerOptionsIds);

            return RedirectToAction(nameof(Edit),  new { id = model.Id });
        }
    }
}