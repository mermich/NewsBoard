using ApiTools.HttpTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.IconSearch
{
    public class DefaultIconSearch : AIconSearch
    {
        private List<string> htmlNodepatterns;

        public DefaultIconSearch(HtmlDocumentPageWrapper doc) : this(doc, new List<string>
        {
            "//link[@type='image/icon']",
            "//link[@type='image/x-icon']",
            "//link[@rel='shortcut icon']",
            "//link[@rel='icon']"
        })
        {

        }

        public DefaultIconSearch(HtmlDocumentPageWrapper doc, List<string> htmlNodepatterns) : base(doc)
        {
            this.htmlNodepatterns = htmlNodepatterns;
        }


        private string findUrlByNodePatterns()
        {
            string iconUrl = null;

            foreach (var item in htmlNodepatterns)
            {
                var node = doc.GetNodesByExpression(item).FirstOrDefault();
                if (node != null)
                {
                    iconUrl = node.GetAttributeValue("href");
                }
            }

            return iconUrl;
        }

        public override Uri GetIconUri()
        {
            var iconUrl = findUrlByNodePatterns();
            if(iconUrl == null)
            {
                iconUrl = "/favicon.ico";
            }

            var uri = new UriPart(iconUrl).ToFullUri(doc.Uri);

            if (new UriTest(uri).DoesExist())
                return uri;
            else
                return null;
        }

        public override bool IsMatch()
        {
            return false;
        }
    }
}
