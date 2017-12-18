namespace SurveyMaker.Services.Models.Question
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class ListingQuestionsServiceModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Poll Poll { get; set; }

        public int PollId { get; set; }

        public IEnumerable<AnswerOption> AnswerOptions { get; set; }
    }
}
