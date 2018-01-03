namespace SurveyMaker.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class TempDataExtensions
    {
        public static ITempDataDictionary AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.SuccessMessage] = message;

            return tempData;
        }
    }

}
