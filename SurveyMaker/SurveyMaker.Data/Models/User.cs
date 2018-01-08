namespace SurveyMaker.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        //[Required]
        //[MinLength(UserNameMinLength)]
        //[MaxLength(UserNameMaxLength)]
        //public string FirstName { get; set; }

        //[Required]
        //[MinLength(UserNameMinLength)]
        //[MaxLength(UserNameMaxLength)]
        //public string LastName { get; set; }

        public List<Poll> Polls { get; set; } = new List<Poll>();
    }
}
