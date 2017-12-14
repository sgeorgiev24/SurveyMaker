namespace SurveyMaker.Web.Models.PollViewModels
{
    using Data.Models;
    using System.Collections.Generic;

    public class EditPollViewModel : CreatePollFormModel
    {
        public IEnumerable<Question> Questions { get; set; }
    }
}
