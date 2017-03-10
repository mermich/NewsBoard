using DiscoverWebSiteApi.HttpTools;
using DiscoverWebSiteApi.Syndication;
using System.Linq;

namespace DiscoverWebSiteApi
{
    public class SyndicationApi
    {
        public SyndicationContent GetSyndication(string syndicationAdress)
        {
            var xDocument = new HttpClientWrapper(syndicationAdress).GetResponse().ToXDocument();

            SyndicationClient client = null;
            if (xDocument.Elements().Any(i => i.Name.LocalName == "feed"))
            {
                client =  new AtomSyndicationClient(xDocument, syndicationAdress);
            }

            if (xDocument.Elements().Any(i => i.Name.LocalName == "rss"))
            {
                client =  new RssSyndicationClient(xDocument, syndicationAdress);
            }

            if (xDocument.Elements().Any(i => i.Name.LocalName == "rdf"))
            {
                client =  new RdfSyndicationClient(xDocument, syndicationAdress);
            }

            return client.SyndicationContent();
        }       
    }
}
