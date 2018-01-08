namespace SurveyMaker.Services
{
    using SurveyMaker.Services.Models.Poll;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPollService
    {
        Task CreateAsync(
            string name, 
            string description, 
            string authorId);

        Task DeleteAsync(int id);

        Task<IEnumerable<PollListingServiceModel>> PollsByUserIdAsync(string userId, string search);

        Task EditAsync(
            int pollId, 
            string name, 
            string description);

        Task<bool> PollExistAsync(int id);

        Task<PollFormServiceModel> PollByIdAsync(int id);

        Task<PollDetailsServiceModel> GetPollDetailsAsync(int id);

        Task<PollCompleteServiceModel> PollByUrlTokenAsync(string urlToken);

        Task SaveDataFromPollAsync(int pollId, Dictionary<string, string> formData);
    }
}
