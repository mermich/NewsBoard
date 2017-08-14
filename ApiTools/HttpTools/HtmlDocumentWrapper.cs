using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiTools.HttpTools
{
    public class HtmlDocumentWrapper
    {
        private HtmlDocument htmlDocument;

        public HtmlDocumentWrapper(Stream stream)
        {
            htmlDocument = new HtmlDocument();
            htmlDocument.Load(stream);
        }

        public HtmlDocumentWrapper(string response)
        {
            htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);
        }

        public IEnumerable<HtmlNode> GetNodes(string nodeName)
        {
            return GetNodesByExpression("//" + nodeName);
        }


        public List<HtmlNode> GetNodesByExpression(string xpathExpression)
        {
            var result = new List<HtmlNode>();
            var nodes = htmlDocument.DocumentNode.SelectNodes(xpathExpression);
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    result.Add(node);
                }
            }
            return result;
        }

        public HtmlNode GetFirstNodesByExpression(string xpathExpression)
        {
            return GetNodesByExpression(xpathExpression).FirstOrDefault();
        }
    }


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
