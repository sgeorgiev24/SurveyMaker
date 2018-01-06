namespace SurveyMaker.Services.Admin.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserListingsServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public IList<string> Role { get; set; }
    }
}
