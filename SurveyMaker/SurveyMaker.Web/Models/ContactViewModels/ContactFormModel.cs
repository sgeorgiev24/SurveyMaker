namespace SurveyMaker.Web.Models.ContactViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static WebConstants;

    public class ContactFormModel
    {
        [Required]
        [MinLength(ContactNameMinLength)]
        [MaxLength(ContactNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(ContactSubjectMinLength)]
        [MaxLength(ContactSubjectMaxLength)]
        public string Subject { get; set; }

        [Required]
        [MinLength(ContactMessageMinLength)]
        [MaxLength(ContactMessageMaxLength)]
        public string Message { get; set; }
    }
}
