namespace SurveyMaker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Authorize]
    public class AnswersController : Controller
    {
        private readonly IAnswerService answers;

        public AnswersController(IAnswerService answers)
        {
            this.answers = answers;
        }

        [HttpGet]
        public IActionResult Delete(int id, string questionId)
        {
            if (!this.answers.AnswerExist(id))
            {
                return NotFound();
            }

            this.answers.Delete(id);

            return RedirectToAction(nameof(QuestionsController.Edit), "Questions", new { id = questionId });
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