namespace SurveyMaker.Services.Models.Poll
{
    using Common.Mapping;
    using Data.Models;
    using Services.Models.Question;
    using System.Collections.Generic;

    public class PollCompleteServiceModel : IMapFrom<Poll>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<QuestionFormServiceModel> Questions { get; set; }
    }
}
