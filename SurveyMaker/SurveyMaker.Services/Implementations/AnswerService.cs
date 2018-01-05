namespace SurveyMaker.Services.Implementations
{
    using Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class AnswerService : IAnswerService
    {
        private readonly SurveyMakerDbContext db;

        public AnswerService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public bool AnswerExist(int id)
            => this.db.AnswerOptions.Any(ao => ao.Id == id);

        public async Task DeleteAsync(int id)
        {
            var answerOption = await this.db.AnswerOptions.FindAsync(id);

            
            this.db.Remove(answerOption);
            await this.db.SaveChangesAsync();
        }
    }
}
