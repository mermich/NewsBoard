using System;

namespace ApiTools.HttpTools
{
    public class XDocumentPageWrapper : XDocumentWrapper
    {
        public Uri Uri;


        public XDocumentPageWrapper(Uri uri, string html) : base(html)
        {
            Uri = uri;
        }
    }
}
