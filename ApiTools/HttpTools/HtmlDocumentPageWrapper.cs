using System;

namespace ApiTools.HttpTools
{
    public class HtmlDocumentPageWrapper: HtmlDocumentWrapper
    {
        public Uri Uri;


        public HtmlDocumentPageWrapper(Uri uri, string html) : base(html)
        {
            Uri = uri;
        }

    }
}
