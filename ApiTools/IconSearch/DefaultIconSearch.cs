using ApiTools.HttpTools;
using System;
using System.Linq;

namespace ApiTools.IconSearch
{
    public class DefaultIconSearch : AIconSearch
    {
        public DefaultIconSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {

        }

        public override Uri GetIconUri()
        {
            var tryChain = new TryChain<string>(() => doc.GetNodesByExpression("//link[@type='image/icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@type='image/x-icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@type='image/x-icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@rel='shortcut icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@rel='icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => "/favicon.ico");

            var uriPart = tryChain.Result;
            var uri = new UriPart(uriPart).ToFullUri(doc.Uri);

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
