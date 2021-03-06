﻿namespace SurveyMaker.Services.Models.Poll
{
    using Common.Mapping;
    using Data.Models;
    using SurveyMaker.Services.Models.Question;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PollFormServiceModel : IMapFrom<Poll>
    {
        public int Id { get; set; }

        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public IEnumerable<ListingQuestionsServiceModel> Questions { get; set; }
    }
}
