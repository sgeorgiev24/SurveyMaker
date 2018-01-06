namespace SurveyMaker.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;
    using System.Threading.Tasks;

    public class PollsController : BaseAdminController
    {
        private readonly IAdminPollService adminPolls;

        public PollsController(IAdminPollService adminPolls)
        {
            this.adminPolls = adminPolls;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.adminPolls.AllAsync();

            return View(model);
        }
    }
}