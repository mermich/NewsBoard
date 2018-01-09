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


        public abstract bool IsMatch();


        public abstract Uri GetSyndicationUri();
    }
}
