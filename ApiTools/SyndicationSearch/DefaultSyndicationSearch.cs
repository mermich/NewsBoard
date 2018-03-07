using ApiTools.HttpTools;
using System;
using System.Linq;

namespace ApiTools.SyndicationSearch
{
    public class DefaultSyndicationSearch : ASyndicationSearch
    {
        public DefaultSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {

        }

        public override Uri GetSyndicationUri()
        {
            var tryChain = new TryChain<Uri>(() =>
            {
                var feedNode = doc.GetNodesByExpression("//link[@type='application/rss+xml'] | //link[@type='application/atom+xml']").FirstOrDefault().GetAttributeValue("href");
                return new UriPart(feedNode).ToFullUri(doc.Uri);
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "rss.xml");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "atom.xml");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "feed.xml");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "feed.rss");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "/rss.xml");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "/atom.xml");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "/feed.xml");
            })
            .ThenTry(() =>
            {
                return TryUriPart(doc.Uri, "/feed.rss");
            });

            if (tryChain.IsSucessFull)
            {
                return tryChain.Result;
            }
            else
            {
                throw new BusinessLogicException("Cannot find syndication");
            }
        }


        public Uri TryUriPart(Uri uri, string part)
        {
            var testUri = new Uri(doc.Uri, part);
            new HttpClientWrapper(testUri).FetchResponse();
            return testUri;
        }

        public override int MatchScore()
        {
            return 1;
        }
    }
}
