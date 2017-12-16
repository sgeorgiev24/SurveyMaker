namespace SurveyMaker.Services.Implementations
{
    using Data;
    using SurveyMaker.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class QuestionService : IQuestionService
    {
        private readonly SurveyMakerDbContext db;

        public QuestionService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public void Create(
            int pollId, 
            string title, 
            IEnumerable<string> answerOptions)
        {
            var question = new Question
            {
                Title = title,
                PollId = pollId
            };
            this.db.Add(question);

            var allAnswerOptions = new List<AnswerOption>();

            foreach (var answerOption in answerOptions)
            {
                var answerOptionDb = new AnswerOption
                {
                    QuestionId = question.Id,
                    Text = answerOption
                };

                this.db.Add(answerOptionDb);

                allAnswerOptions.Add(answerOptionDb);
            }

            this.db.SaveChanges();
        }

        public bool QuestionExist(int id)
            => this.db.Questions.Any(q => q.Id == id);
    }
}
