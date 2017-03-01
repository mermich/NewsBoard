using SiteParser.Client;
using System;
using System.Linq;

namespace SiteParser
{
    public class FeedClientStrategy
    {
        XDocumentWrapper xDocumentWrapper;
        string syndicationURl;



        public FeedClientStrategy(XDocumentWrapper xDocumentWrapper, string syndicationURl)
        {
            this.xDocumentWrapper = xDocumentWrapper;
            this.syndicationURl = syndicationURl;
        }


        public AFeedClient FeedClient()
        {
            var document = xDocumentWrapper.GetDocument();

            if (document.Elements().Any(i => i.Name.LocalName == "feed"))
            {
                return new AtomFeedClient(document, syndicationURl);
            }

            if (document.Elements().Any(i => i.Name.LocalName == "rss"))
            {
                return new RssFeedClient(document, syndicationURl);
            }

            if (document.Elements().Any(i => i.Name.LocalName == "rdf"))
            {
                return new RdfFeedClient(document, syndicationURl);
            }


            throw new NotSupportedException();
        }
    }
}