using System.Xml.Linq;

namespace SiteParser
{
    public class XDocumentWrapper
    {
        XDocument doc;

        public XDocumentWrapper(XDocument doc)
        {
            this.doc = doc;
        }


        public XDocumentWrapper(string content)
        {
            doc = XDocument.Parse(content);
        }

        public XDocument GetDocument()
        {
            return doc;
        }

        public FeedClientStrategy ToFeedClientStrategy(string syndicationUrl)
        {
            return new FeedClientStrategy(this, syndicationUrl);
        }
    }
}
