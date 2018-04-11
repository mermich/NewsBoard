using ApiTools.HttpTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.SyndicationSearch
{
    public class DefaultSyndicationSearch : ASyndicationSearch
    {
        public DefaultSyndicationSearch(HtmlDocumentPageWrapper doc, List<string> patterns = null) : base(doc)
        {
            if (patterns != null)
            {
                Patterns = patterns;
            }
        }

        private List<string> Patterns = new List<string>(){
            "feed.xml",
            "feed.rss",
            "/feed.xml",
            "/feed.rss",
            "rss.xml",
            "/rss.xml",
            "atom.xml",
            "/atom.xml"
        };

        public List<Uri> BuildRelativeUris()
        {
            return Patterns.Select(p => BuildRelativeUri(doc.Uri, p)).ToList();
        }

        public List<Uri> BuildBaseUris()
        {
            return Patterns.Select(p => BuildBaseUri(doc.Uri, p)).ToList();
        }

        public Uri FindSyndicationUriByHtmlContent()
        {
            var node = doc.GetNodesByExpression("//link[@type='application/rss+xml'] | //link[@type='application/atom+xml']").FirstOrDefault();
            if (node != null)
            {
                var nodeUrl = node.GetAttributeValue("href");
                var nodeUri = new UriPart(nodeUrl).ToFullUri(doc.Uri);
                if (new UriTest(nodeUri).DoesExist())
                {
                    return nodeUri;
                }
            }

            return null;
        }

        public Uri FindFindSyndicationUriByCommunUri()
        {
            var urisToTest = new List<Uri>();
            urisToTest.AddRange(BuildRelativeUris());
            urisToTest.AddRange(BuildBaseUris());

            var uri = urisToTest.FirstOrDefault(u => new UriTest(u).DoesExist());

            return uri;
        }

        public override Uri GetSyndicationUri()
        {
            var uriByHtmlContent = FindSyndicationUriByHtmlContent();
            if (uriByHtmlContent != null)
            {
                return uriByHtmlContent;
            }

            var uriByCommunUri = FindFindSyndicationUriByCommunUri();
            if (uriByCommunUri != null)
            {
                return uriByCommunUri;
            }

            throw new BusinessLogicException("Cannot find syndication");
        }


        public Uri BuildRelativeUri(Uri uri, string part)
        {
            return new Uri(doc.Uri + part);
        }

        public Uri BuildBaseUri(Uri uri, string part)
        {
            return new Uri(doc.Uri, part);
        }

        public override int MatchScore()
        {
            return 1;
        }
    }
}
