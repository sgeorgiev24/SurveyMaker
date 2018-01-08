namespace SurveyMaker.Services.Implementations
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AnswerService : IAnswerService
    {
        private readonly SurveyMakerDbContext db;

        public AnswerService(SurveyMakerDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AnswerExistAsync(int id)
            => await this.db
                .AnswerOptions
                .AnyAsync(ao => ao.Id == id);

        public async Task DeleteAsync(int id)
        {
            var answerOption = await this.db.AnswerOptions.FindAsync(id);
            
            this.db.Remove(answerOption);
            await this.db.SaveChangesAsync();
        }
    }
}
