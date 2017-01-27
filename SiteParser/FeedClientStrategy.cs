using SiteParser.Client;
using System;
using System.Linq;

namespace SiteParser
{
    public class FeedClientStrategy
    {
        XDocumentWrapper document;
        string syndicationURl;



        public FeedClientStrategy(XDocumentWrapper document, string syndicationURl)
        {
            this.document = document;
            this.syndicationURl = syndicationURl;
        }


        public AFeedClient FeedClient()
        {
            var document1 = document.GetDocument();

            if (document1.Elements().Any(i => i.Name.LocalName == "feed"))
            {
                return new AtomFeedClient(document1, syndicationURl);
            }

            if (document1.Elements().Any(i => i.Name.LocalName == "rss"))
            {
                return new RssFeedClient(document1, syndicationURl);
            }

            if (document1.Elements().Any(i => i.Name.LocalName == "rdf"))
            {
                return new RdfFeedClient(document1, syndicationURl);
            }


            throw new NotSupportedException();
        }
    }
}