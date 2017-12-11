namespace SurveyMaker.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public List<Poll> Polls { get; set; } = new List<Poll>();
    }
}
