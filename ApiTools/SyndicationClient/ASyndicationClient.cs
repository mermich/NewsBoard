using ApiTools.HttpTools;

namespace ApiTools.SyndicationClient
{
    abstract public class ASyndicationClient
    {
        protected XDocumentPageWrapper doc;

        public ASyndicationClient(XDocumentPageWrapper doc)
        {
            this.doc = doc;
        }

        public abstract SyndicationContent GetSyndicationContent();


        public abstract bool IsMatch();
    }
}
