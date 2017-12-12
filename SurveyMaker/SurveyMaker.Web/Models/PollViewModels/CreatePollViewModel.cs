namespace SurveyMaker.Web.Models.PollViewModels
{
    using Common.Mapping;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CreatePollViewModel : IMapFrom<Poll>
    {
        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
