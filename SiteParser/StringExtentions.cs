using System.Text.RegularExpressions;

namespace SiteParser
{
    internal static class StringExtentions
    {
        internal static string RemoveHtmlTags(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty); ;
        }

        internal static string SafeSubtring(this string input, int maxLength)
        {
            if (input.Length > maxLength)
                return input.Substring(0, maxLength);

            return input;
        }
    }
}
