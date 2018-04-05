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


        /// <summary>
        /// 0 doenst match, 100 match, more than 100 to take higher priority.
        /// </summary>
        /// <returns></returns>
        public abstract int MatchScore();
    }
}
