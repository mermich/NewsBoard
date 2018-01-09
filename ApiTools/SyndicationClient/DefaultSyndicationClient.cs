using System;
using ApiTools.HttpTools;

namespace ApiTools.SyndicationClient
{
    public class DefaultSyndicationClient : ASyndicationClient
    {
        public DefaultSyndicationClient(XDocumentPageWrapper doc) : base( doc)
        {
        }

        public override bool IsMatch()
        {
            throw new NotImplementedException();
        }

        public override SyndicationContent GetSyndicationContent()
        {
            throw new NotImplementedException();
        }
    }
}
