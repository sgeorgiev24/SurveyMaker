namespace SurveyMaker.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Question
    {
        public int Id { get; set; }

        [MinLength(QuestionTitleMinLength)]
        [MaxLength(QuestionTitleMaxLength)]
        public string Title { get; set; }

        public Poll Poll { get; set; }

        public int PollId { get; set; }

        public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
    }
}
