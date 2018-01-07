namespace SurveyMaker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string questionId, string pollId)
        {
            if (!await this.answers.AnswerExistAsync(id))
            {
                return NotFound();
            }
            if (await this.questions.AnswersCountAsync(int.Parse(questionId)) <= 2)
            {
                return BadRequest();
            }

            await this.answers.DeleteAsync(id);

            TempData.AddSuccessMessage("Answer deleted.");

            if (string.IsNullOrEmpty(pollId))
            {
                return RedirectToAction(nameof(QuestionsController.Edit), "Questions", new { id = int.Parse(questionId) });
            }
            return RedirectToAction(nameof(PollsController.Edit), "Polls", new { id = int.Parse(pollId) });
        }
    }
}