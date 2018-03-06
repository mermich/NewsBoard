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
                var testUri = new Uri(doc.Uri, "rss.xml");
                new HttpClientWrapper(testUri).FetchResponse();
                return testUri;
            })
            .ThenTry(() =>
            {
                var testUri = new Uri(doc.Uri, "atom.xml");
                new HttpClientWrapper(testUri).FetchResponse();
                return testUri;
            })
            .ThenTry(() =>
            {
                var testUri = new Uri(doc.Uri, "feed.xml");
                new HttpClientWrapper(testUri).FetchResponse();
                return testUri;
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

        public override int MatchScore()
        {
            return 1;
        }
    }
}
