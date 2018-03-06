using ApiTools.HttpTools;
using System;

namespace ApiTools.SyndicationSearch
{
    public abstract class ASyndicationSearch
    {
        protected HtmlDocumentPageWrapper doc;

        public ASyndicationSearch(HtmlDocumentPageWrapper doc)
        {
            this.doc = doc;
        }


        /// <summary>
        /// 0 doenst match, 100 match, more than 100 to take higher priority.
        /// </summary>
        /// <returns></returns>
        public abstract int MatchScore();


        public abstract Uri GetSyndicationUri();
    }
}
