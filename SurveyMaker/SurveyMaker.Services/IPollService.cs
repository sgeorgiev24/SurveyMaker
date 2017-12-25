namespace SurveyMaker.Services
{
    using SurveyMaker.Services.Models.Poll;
    using System.Collections.Generic;

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

        PollDetailsServiceModel GetPollDetails(int id);

        PollCompleteServiceModel PollByUrlToken(string urlToken);

        void SaveDataFromPoll(int pollId, IEnumerable<int> answersIds);
    }
}
