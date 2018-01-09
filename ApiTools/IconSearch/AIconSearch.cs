using ApiTools.HttpTools;
using System;

namespace ApiTools.IconSearch
{
    public abstract class AIconSearch
    {
        protected HtmlDocumentPageWrapper doc;

        public AIconSearch(HtmlDocumentPageWrapper doc)
        {
            this.doc = doc;
        }


        public abstract bool IsMatch();


        public abstract Uri GetIconUri();
    }
}
