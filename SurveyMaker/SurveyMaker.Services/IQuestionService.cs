﻿namespace SurveyMaker.Services
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

        Task<bool> QuestionExistAsync(int id);

        Task<QuestionFormServiceModel> QuestionByIdAsync(int id);

        Task EditAsync(int questionId,
            string title,
            IEnumerable<string> answerOptions,
            IEnumerable<int> answerOptionsIds);

        Task<int> AnswersCountAsync(int id);
    }
}
