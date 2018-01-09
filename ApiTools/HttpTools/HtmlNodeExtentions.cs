using HtmlAgilityPack;

namespace ApiTools.HttpTools
{
    public static class HtmlNodeExtentions
    {
        public static HtmlAttribute GetAttribute(this HtmlNode node, string attributeName)
        {
            return node.Attributes[attributeName];
        }

        public static string GetAttributeValue(this HtmlNode node, string attributeName)
        {
            return node.Attributes[attributeName].Value;
        }
    }
}
