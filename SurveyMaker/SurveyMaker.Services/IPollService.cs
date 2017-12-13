namespace SurveyMaker.Services
{
    using SurveyMaker.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPollService
    {
        void Create(
            string name, 
            string description, 
            string authorId);

        IEnumerable<PollListingServiceModel> PollsByUserId(string userId);

        void Edit(
            int pollId, 
            string name, 
            string description);

        bool PollExist(int id);

        PollFormServiceModel PollById(int id);
    }
}
