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

        public override bool IsMatch()
        {
            return doc.Elements().Any(i => i.Name.LocalName == "rdf");
        }

        

        public override SyndicationContent GetSyndicationContent()
        {
            throw new NotImplementedException();
        }
    }
}
