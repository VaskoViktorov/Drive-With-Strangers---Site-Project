namespace DriveWithStrangers.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    /// <summary>
    /// Adds message to the temp data, which can be shown to the user.
    /// </summary>  
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.TempDataSuccessMessageKey] = message;
        }

        public static void AddWarningMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.TempDataWarningMessageKey] = message;
        }
    }
}
