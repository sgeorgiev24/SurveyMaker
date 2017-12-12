namespace SurveyMaker.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PollsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() => View();
    }
}