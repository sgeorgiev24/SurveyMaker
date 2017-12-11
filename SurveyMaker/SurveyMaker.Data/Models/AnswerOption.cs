namespace SurveyMaker.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class AnswerOption
    {
        public int Id { get; set; }

        [MinLength(AnswerOptionTextMinLength)]
        [MaxLength(AnswerOptionTextMaxLength)]
        public string Text { get; set; }

        [Range(0, int.MaxValue)]
        public int Votes { get; set; }

        public Question Question { get; set; }

        public int QuestionId { get; set; }
    }
}
