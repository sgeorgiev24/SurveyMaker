namespace SurveyMaker.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SurveyMaker.Web.Infrastructure.Extensions;
    using SurveyMaker.Web.Infrastructure.Filters;
    using SurveyMaker.Web.Models.ContactViewModels;
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using static WebConstants;

    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Index(ContactFormModel model)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(model.Email);
                message.Subject = model.Subject;
                message.Body = model.Message + Environment.NewLine + "Sender: " + model.Email;
                message.To.Add(ContactSubject);
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;

                smtp.Credentials = new System.Net.NetworkCredential
                (ContactCredentialsEmail, ContactCredentialsPassword);
                smtp.EnableSsl = true;
                smtp.Send(message);

                ModelState.Clear();
                TempData.AddSuccessMessage("Your email is received.");
            }
            catch (Exception ex)
            {
                TempData.AddSuccessMessage(ex.ToString());
            }
            return RedirectToAction("Index", "Home");
        }
    }
}