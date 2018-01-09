using ApiTools.HttpTools;
using System.Linq;
using System;

namespace ApiTools.SyndicationSearch
{
    public class WordPressSyndicationSearch : ASyndicationSearch
    {
        public WordPressSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public override Uri GetSyndicationUri()
        {
            var feedNode = doc.GetNodesByExpression("//link[@type='application/rss+xml'] | //link[@type='application/atom+xml']").FirstOrDefault();
            return new Uri(doc.Uri, feedNode.GetAttributeValue("href"));
        }

        public override bool IsMatch()
        {
            bool isWordpress = doc.GetNodesByExpression("//meta[@content]").Any(m => m.GetAttributeValue("content").Contains("WordPress"));
            return isWordpress;
        }
    }
}
