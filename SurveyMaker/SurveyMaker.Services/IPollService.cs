namespace SurveyMaker.Services
{
    using System.Threading.Tasks;

    public interface IPollService
    {
        Task CreateAsync(
            string name, 
            string description, 
            string authorId);
    }
}
