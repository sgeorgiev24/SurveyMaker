namespace SurveyMaker.Services.Models.Question
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class EditQuestionServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public IEnumerable<string> AnswerOptions { get; set; }

        public IEnumerable<int> AnswerOptionsIds { get; set; }
    }
}
