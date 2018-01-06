namespace SurveyMaker.Services.Models.Poll
{
    using Common.Mapping;
    using Data.Models;
    using Services.Models.Question;
    using System.Collections.Generic;

    public class PollDetailsServiceModel: IMapFrom<Question>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string UrlToken { get; set; }

        public string AuthorId { get; set; }
        
        public int UsersCompleted { get; set; }

        public IEnumerable<ListingQuestionsServiceModel> Questions { get; set; }
    }
}
