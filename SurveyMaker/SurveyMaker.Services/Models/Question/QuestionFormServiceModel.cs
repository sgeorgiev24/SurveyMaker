namespace SurveyMaker.Services.Models.Question
{
    using Data.Models;
    using SurveyMaker.Common.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class QuestionFormServiceModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(QuestionTitleMinLength)]
        [MaxLength(QuestionTitleMaxLength)]
        public string Title { get; set; }

        [Display(Name = "Answer Option")]
        public ICollection<AnswerOption> AnswerOptions { get; set; }
    }
}
