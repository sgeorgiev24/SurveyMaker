namespace SurveyMaker.Services.Models
{
    using Common.Mapping;
    using Data.Models;

    public class CreatePollServiceModel : IMapFrom<Poll>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string UrlToken { get; set; }

        public string AuthorId { get; set; }
    }
}
