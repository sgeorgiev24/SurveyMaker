namespace SurveyMaker.Web.Models.PollViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CreatePollFormViewModel
    {
        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
