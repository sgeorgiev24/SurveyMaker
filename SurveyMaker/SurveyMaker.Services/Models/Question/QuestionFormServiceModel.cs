namespace SurveyMaker.Services.Models.Question
{
    using Data.Models;
    using SurveyMaker.Common.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    using AutoMapper;

    public class QuestionFormServiceModel : IMapFrom<Question>, IHaveCustomMapping
    {
        public int Id { get; set; }

        [Required]
        [MinLength(QuestionTitleMinLength)]
        [MaxLength(QuestionTitleMaxLength)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        [Display(Name = "Answer Option")]
        public ICollection<AnswerOption> AnswerOptions { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Question, QuestionFormServiceModel>()
                .ForMember(q => q.AuthorId, cfg => cfg.MapFrom(q => q.Poll.AuthorId));
    }
}
