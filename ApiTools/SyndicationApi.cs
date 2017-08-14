using ApiTools.HttpTools;
using ApiTools.Syndication;
using System;
using System.Linq;

namespace ApiTools
{
    public class SyndicationApi
    {
        public SyndicationContent GetSyndication(string syndicationAdress)
        {
            if (string.IsNullOrWhiteSpace(syndicationAdress))
            {
                return new SyndicationContent();
            }
               
            var uri = new Uri(syndicationAdress);
            var xDocument = new HttpClientWrapper(uri).ToXDocument();

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
