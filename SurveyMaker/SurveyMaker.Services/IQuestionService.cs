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

        Task DeleteAsync(int id);

        bool QuestionExist(int id);

        Task<QuestionFormServiceModel> QuestionByIdAsync(int id);

        void Edit(int questionId,
            string title,
            IEnumerable<string> answerOptions,
            IEnumerable<int> answerOptionsIds);

        int AnswersCount(int id);
    }
}
