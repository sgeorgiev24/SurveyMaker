namespace SurveyMaker.Services
{
    public interface IAnswerService
    {
        void Delete(int id);

        bool AnswerExist(int id);
    }
}
