namespace SurveyMaker.Services.Admin.Models.Poll
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class PollListingsServiceModel : IMapFrom<Poll>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UsersCompleted { get; set; }

        public string Author { get; set; }

        public int QuestionsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Poll, PollListingsServiceModel>()
                .ForMember(p => p.QuestionsCount, cfg => cfg
                    .MapFrom(p => p.Questions.Count))
                .ForMember(p => p.Author, cfg => cfg
                    .MapFrom(p => p.Author.UserName));
    }
}
