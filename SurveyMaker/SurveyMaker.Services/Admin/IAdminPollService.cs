namespace SurveyMaker.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Poll;

    public interface IAdminPollService
    {
        Task<IEnumerable<PollListingsServiceModel>> AllAsync();
    }
}
