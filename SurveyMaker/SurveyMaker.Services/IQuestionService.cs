namespace SurveyMaker.Services
{
    using Services.Models.Question;
    using System.Collections.Generic;

    public interface IQuestionService
    {
        void Create(
            int pollId,
            string title,
            IEnumerable<string> answerOptions);

        bool QuestionExist(int id);

        QuestionFormServiceModel QuestionById(int id);

        void Edit(int questionId,
            string title,
            IEnumerable<string> answerOptions,
            IEnumerable<int> answerOptionsIds);

        int AnswersCount(int id);
    }
}
