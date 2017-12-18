namespace SurveyMaker.Services.Implementations
{
    using Data;
    using System.Linq;

    public class AnswerService : IAnswerService
    {
        private readonly SurveyMakerDbContext db;

        public AnswerService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public bool AnswerExist(int id)
            => this.db.AnswerOptions.Any(ao => ao.Id == id);

        public void Delete(int id)
        {
            var answerOption = this.db.AnswerOptions.Find(id);
            
            this.db.Remove(answerOption);
            this.db.SaveChanges();
        }
    }
}
