namespace SurveyMaker.Services
{
    using Services.Models.Question;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionService
    {
        Task CreateAsync(
            int pollId,
            string title,
            IEnumerable<string> answerOptions);

        void Delete(int id);

        bool QuestionExist(int id);

        QuestionFormServiceModel QuestionById(int id);

        void Edit(int questionId,
            string title,
            IEnumerable<string> answerOptions,
            IEnumerable<int> answerOptionsIds);

        int AnswersCount(int id);
    }
}
