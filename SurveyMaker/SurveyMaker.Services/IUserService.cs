namespace SurveyMaker.Services
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> HaveAccessAsync(string authorId);
    }
}
