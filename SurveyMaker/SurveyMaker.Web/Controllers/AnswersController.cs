namespace SurveyMaker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Authorize]
    public class AnswersController : Controller
    {
        private readonly IAnswerService answers;
        private readonly IQuestionService questions;

        public AnswersController(
            IAnswerService answers,
            IQuestionService questions)
        {
            this.answers = answers;
            this.questions = questions;
        }

        [HttpGet]
        public IActionResult Delete(int id, string questionId)
        {
            if (!this.answers.AnswerExist(id))
            {
                return NotFound();
            }
            if (this.questions.AnswersCount(int.Parse(questionId)) <= 2)
            {
                return BadRequest();
            }

            this.answers.Delete(id);

            return RedirectToAction(nameof(QuestionsController.Edit), "Questions", new { id = int.Parse(questionId) });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id, string returnUrl)
        {
            if (!this.answers.AnswerExist(id))
            {
                return NotFound();
            }

            this.answers.Delete(id);

            return RedirectToAction(nameof(QuestionsController.Edit), "Questions", new { id = 2 });
        }
    }
}