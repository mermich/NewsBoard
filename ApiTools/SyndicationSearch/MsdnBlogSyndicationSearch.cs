using ApiTools.HttpTools;
using System;

namespace ApiTools.SyndicationSearch
{
    public class MsdnBlogSyndicationSearch : ASyndicationSearch
    {
        public MsdnBlogSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public override Uri GetSyndicationUri()
        {
            return new Uri(doc.Uri.Scheme + "://" + doc.Uri.Host + doc.Uri.Segments[0] + doc.Uri.Segments[1] + "feed");
        }

        public override int MatchScore()
        {
            if (doc.Uri.Host == "blogs.msdn.microsoft.com")
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
    }
}
