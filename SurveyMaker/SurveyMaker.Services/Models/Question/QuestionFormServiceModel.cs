namespace SurveyMaker.Services.Models.Question
{
    using Data.Models;
    using SurveyMaker.Common.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class QuestionFormServiceModel : IMapFrom<Question>
    {
        [Required]
        [MinLength(QuestionTitleMinLength)]
        [MaxLength(QuestionTitleMaxLength)]
        public string Title { get; set; }

        [Display(Name = "Answer Option")]
        public IEnumerable<AnswerOption> AnswerOptions { get; set; }
    }
}
