namespace SurveyMaker.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class PollListingServiceModel : IMapFrom<Poll>//, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int UsersCompleted { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int Questions { get; set; }

        //public void ConfigureMapping(Profile mapper)
        //    => mapper
        //        .CreateMap<Poll, PollListingServiceModel>()
        //        .ForMember(p => p.Questions, cfg => cfg
        //            .MapFrom(p => p.Questions.Count))
        //        .ForMember(p => p.Author, cfg => cfg
        //            .MapFrom(p => p.Author.UserName));
    }
}
