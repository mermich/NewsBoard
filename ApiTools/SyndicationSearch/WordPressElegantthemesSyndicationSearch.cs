using ApiTools.HttpTools;
using System.Linq;
using System;

namespace ApiTools.SyndicationSearch
{
    public class WordPressElegantThemesSyndicationSearch : ASyndicationSearch
    {
        public WordPressElegantThemesSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public override Uri GetSyndicationUri()
        {
            return new Uri(doc.Uri, "/feed");
        }

        public override int MatchScore()
        {
            bool isWordpress = doc.GetNodesByExpression("//meta[@content]").Any(m => m.GetAttributeValue("content").Contains("WordPress"));

            bool isElegantTheme = doc.GetNodesByExpression("//div[@id]").Any(m => m.GetAttributeValue("id").Contains("et-top-navigation"));

            if (isWordpress && isElegantTheme)
            {
                return 150;
            }
            else
            {
                return 0;
            }
        }
    }
}
