namespace SurveyMaker.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using SurveyMaker.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using SurveyMaker.Services.Models.Question;
    using System.Threading.Tasks;

    public class QuestionService : IQuestionService
    {
        private readonly SurveyMakerDbContext db;

        public QuestionService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public int AnswersCount(int id)
            => this.db.AnswerOptions
                .Where(a => a.QuestionId == id)
                .ToList()
                .Count;

        public async Task CreateAsync(
            int pollId, 
            string title, 
            IEnumerable<string> answerOptions)
        {
            var question = new Question
            {
                Title = title,
                PollId = pollId
            };
            await this.db.AddAsync(question);

            var allAnswerOptions = new List<AnswerOption>();

            foreach (var answerOption in answerOptions)
            {
                var answerOptionDb = new AnswerOption
                {
                    QuestionId = question.Id,
                    Text = answerOption
                };

                await this.db.AddAsync(answerOptionDb);

                allAnswerOptions.Add(answerOptionDb);
            }

            await this.db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var question = this.db.Questions.Find(id);

            this.db.Remove(question);
            this.db.SaveChanges();
        }

        public void Edit(int questionId, string title, IEnumerable<string> answerOptions, IEnumerable<int> answerOptionsIds)
        {
            var question = this.db.Questions.Find(questionId);

            question.Title = title;

            var answers = new List<string>(answerOptions);

            foreach (var answerId in answerOptionsIds)
            {
                var answer = this.db.AnswerOptions.Find(answerId);
                foreach (var answerText in answers.ToList())
                {
                    answer.Text = answerText;
                    answers.Remove(answerText);
                    break;
                }
                this.db.SaveChanges();
            }
            foreach (var answer in answers)
            {
                var answerOptionDb = new AnswerOption
                {
                    QuestionId = questionId,
                    Text = answer
                };

                this.db.Add(answerOptionDb);
            }

            this.db.SaveChanges();
        }

        public QuestionFormServiceModel QuestionById(int id)
            => this.db.Questions
                .Where(q => q.Id == id)
                .ProjectTo<QuestionFormServiceModel>()
                .FirstOrDefault();

        public bool QuestionExist(int id)
            => this.db.Questions.Any(q => q.Id == id);
    }
}
