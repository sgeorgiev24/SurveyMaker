﻿namespace SurveyMaker.Web.Controllers
{
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.QuestionViewModels;
    using Services;
    using Services.Models.Question;
    using SurveyMaker.Web.Infrastructure.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService questions;
        private readonly IUserService users;

        public QuestionsController(
            IQuestionService questions,
            IUserService users)
        {
            this.questions = questions;
            this.users = users;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create([FromQuery]int pollId, CreateQuestionFormViewModel model)
        {
            await this.questions.CreateAsync(pollId, model.Title, model.AnswerOptions);

            TempData.AddSuccessMessage($"Question \"{model.Title}\" created.");

            return RedirectToAction(nameof(PollsController.Edit), "Polls", new { id = pollId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id, [FromForm]string pollId)
        {
            if (!await this.questions.QuestionExistAsync(id))
            {
                return NotFound();
            }

            await this.questions.DeleteAsync(id);

            TempData.AddSuccessMessage("Question deleted.");

            return RedirectToAction(nameof(PollsController.Edit), "Polls", new { id = int.Parse(pollId) });
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.questions.QuestionExistAsync(id))
            {
                return NotFound();
            }

            var model = await this.questions.QuestionByIdAsync(id);

            if (!await this.users.HaveAccessAsync(model.AuthorId))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, EditQuestionServiceModel model)
        {
            await this.questions.EditAsync(id, model.Title, model.AnswerOptions, model.AnswerOptionsIds);

            TempData.AddSuccessMessage($"Question \"{model.Title}\" edited.");

            return RedirectToAction(nameof(Edit),  new { id = model.Id });
        }
    }
}