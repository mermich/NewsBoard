using System.Text.RegularExpressions;

namespace NewBoardRestApi.Syndication
{
    public static class StringExtentions
    {
        public static string RemoveHtmlTags(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty); ;
        }

        public static string SafeSubtring(this string input, int maxLength)
        {
            if (input.Length > maxLength)
                return input.Substring(0, maxLength);

            return input;
        }
    }
}
