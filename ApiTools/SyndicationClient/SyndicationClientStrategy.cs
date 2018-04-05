using ApiTools.HttpTools;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.SyndicationClient
{
    public class SyndicationClientStrategy
    {
        List<ASyndicationClient> clients = new List<ASyndicationClient>();

        public SyndicationClientStrategy(XDocumentPageWrapper doc) : this(new List<ASyndicationClient> { new RssSyndicationClient(doc), new AtomSyndicationClient(doc), new RdfSyndicationClient(doc), new DefaultSyndicationClient(doc) })
        {
        }

        public SyndicationClientStrategy(IList<ASyndicationClient> clients)
        {
            this.clients.AddRange(clients);
        }

        public ASyndicationClient GetSyndicationClient()
        {
            return clients.OrderBy(f => f.MatchScore()).LastOrDefault();
        }
    }
}
