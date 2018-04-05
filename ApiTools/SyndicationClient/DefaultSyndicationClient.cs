using System;
using ApiTools.HttpTools;

namespace ApiTools.SyndicationClient
{
    public class DefaultSyndicationClient : ASyndicationClient
    {
        public DefaultSyndicationClient(XDocumentPageWrapper doc) : base(doc)
        {
        }

        public override int MatchScore()
        {
            return 1;
        }

        public override SyndicationContent GetSyndicationContent()
        {
            throw new NotImplementedException();
        }
    }
}
