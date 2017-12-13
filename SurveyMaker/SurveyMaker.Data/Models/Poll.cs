namespace SurveyMaker.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Poll
    {
        public int Id { get; set; }

        [Required]
        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string UrlToken { get; set; }

        [Range(0, int.MaxValue)]
        public int UsersCompleted { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
