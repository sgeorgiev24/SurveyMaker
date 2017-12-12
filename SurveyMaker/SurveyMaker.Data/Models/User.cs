namespace SurveyMaker.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        public List<Poll> Polls { get; set; } = new List<Poll>();
    }
}
