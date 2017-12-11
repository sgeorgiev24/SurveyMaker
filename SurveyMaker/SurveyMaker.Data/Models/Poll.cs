﻿namespace SurveyMaker.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Poll
    {
        public int Id { get; set; }

        [MinLength(PollNameMinLength)]
        [MaxLength(PollNameMaxLength)]
        public string Name { get; set; }

        [MinLength(PollDescriptionMinLength)]
        [MaxLength(PollDescriptionMaxLength)]
        public string Description { get; set; }

        public string UrlToken { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalVotes { get; set; }

        [Range(0, int.MaxValue)]
        public int UsersCompleted { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
