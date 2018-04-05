using ApiTools.HttpTools;
using System;
using System.Linq;

namespace ApiTools.SyndicationClient
{
    public class RdfSyndicationClient : ASyndicationClient
    {
        public RdfSyndicationClient(XDocumentPageWrapper doc) : base( doc)
        {
        }

        public override SyndicationContent GetSyndicationContent()
        {
            throw new NotImplementedException();
        }

        public override int MatchScore()
        {
            var isMatch = doc.Elements().Any(i => i.Name.LocalName == "rdf");
            if (isMatch)
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
    }
}
