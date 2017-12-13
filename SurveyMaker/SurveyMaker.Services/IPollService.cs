namespace SurveyMaker.Services
{
    using SurveyMaker.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPollService
    {
        Task CreateAsync(
            string name, 
            string description, 
            string authorId);

        IEnumerable<PollListingServiceModel> PollByUserId(string userId);

        EditPollServiceModel Edit(int pollId);
    }
}
