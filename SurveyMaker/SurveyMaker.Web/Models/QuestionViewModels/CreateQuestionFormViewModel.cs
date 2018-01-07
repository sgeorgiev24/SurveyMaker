namespace SurveyMaker.Web.Models.QuestionViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CreateQuestionFormViewModel
    {
        [Required]
        [MinLength(QuestionTitleMinLength)]
        [MaxLength(QuestionTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Answer Option")]
        public IEnumerable<string> AnswerOptions { get; set; }
    }
}
