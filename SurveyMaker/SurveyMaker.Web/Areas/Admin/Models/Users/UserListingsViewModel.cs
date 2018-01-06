namespace SurveyMaker.Web.Areas.Admin.Models.Users
{
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class UserListingsViewModel
    {
        public IEnumerable<AdminUserListingsServiceModel> Users { get; set; }
    }
}
