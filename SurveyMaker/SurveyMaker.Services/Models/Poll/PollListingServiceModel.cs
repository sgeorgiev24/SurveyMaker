namespace SurveyMaker.Services.Models.Poll
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class PollListingServiceModel : IMapFrom<Poll>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public int QuestionsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Poll, PollListingServiceModel>()
                .ForMember(p => p.QuestionsCount, cfg => 
                    cfg.MapFrom(p => p.Questions.Count));
    }
}
