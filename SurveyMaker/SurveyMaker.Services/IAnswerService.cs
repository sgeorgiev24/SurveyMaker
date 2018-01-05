namespace SurveyMaker.Services
{
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task DeleteAsync(int id);

        Task<bool> AnswerExistAsync(int id);
    }
}
