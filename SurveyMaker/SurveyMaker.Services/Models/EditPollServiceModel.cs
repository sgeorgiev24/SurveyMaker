namespace SurveyMaker.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class EditPollServiceModel
    {
        [Required]
        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }

        //public List<Question> Questions { get; set; } = new List<Question>();
    }
}
