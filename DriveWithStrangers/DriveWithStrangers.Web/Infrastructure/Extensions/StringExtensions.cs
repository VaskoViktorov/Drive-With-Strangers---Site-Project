namespace DriveWithStrangers.Web.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// changes the url text to user friendly url(only words, digits and dashes)
    /// </summary>
    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
            => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();
    }
}
