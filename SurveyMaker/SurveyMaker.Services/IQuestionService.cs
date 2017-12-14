namespace SurveyMaker.Services
{
    using System.Collections.Generic;

    public interface IQuestionService
    {
        void Create(
            int pollId,
            string title,
            IEnumerable<string> answerOptions);
    }
}
