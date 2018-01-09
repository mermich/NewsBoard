using ApiTools.HttpTools;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.SyndicationClient
{
    public class SyndicationClientStrategy
    {
        List<ASyndicationClient> clients = new List<ASyndicationClient>();
        ASyndicationClient defaultSyndicationClient;

        public SyndicationClientStrategy(XDocumentPageWrapper doc) : this(defaultSyndicationClient: new DefaultSyndicationClient(doc), clients: new List<ASyndicationClient> { new RssSyndicationClient(doc), new AtomSyndicationClient(doc), new RdfSyndicationClient(doc) })
        {
        }

        public SyndicationClientStrategy(ASyndicationClient defaultSyndicationClient, IList<ASyndicationClient> clients)
        {          
            this.defaultSyndicationClient = defaultSyndicationClient;
            this.clients.AddRange(clients);
        }

        public ASyndicationClient GetSyndicationClient()
        {
            return clients.FirstOrDefault(f => f.IsMatch());
        }

        public ASyndicationClient GetSyndicationClientOrDefault()
        {
            var finder = clients.FirstOrDefault(f => f.IsMatch());
            if (finder == null)
            {
                finder = defaultSyndicationClient;
            }

            return finder;
        }
    }
}
