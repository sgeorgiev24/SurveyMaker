namespace SurveyMaker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class QuestionsController : Controller
    {
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create([FromRoute]int id)
        {
            return View();
        }
    }
}