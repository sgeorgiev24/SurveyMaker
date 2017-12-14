namespace SurveyMaker.Services.Models
{
    using Data.Models;
    using Common.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PollFormServiceModel : IMapFrom<Poll>
    {
        [Required]
        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }

        public IEnumerable<ListingQuestionsServiceModel> Questions { get; set; }
    }
}
