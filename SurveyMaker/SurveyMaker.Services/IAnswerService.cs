namespace SurveyMaker.Services
{
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task DeleteAsync(int id);

        bool AnswerExist(int id);
    }
}
