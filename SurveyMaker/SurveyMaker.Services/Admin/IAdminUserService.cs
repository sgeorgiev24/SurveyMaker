namespace SurveyMaker.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingsServiceModel>> AllAsync();
    }
}
