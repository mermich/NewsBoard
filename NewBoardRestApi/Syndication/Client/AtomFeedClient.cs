using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NewBoardRestApi.Syndication.Client
{
    public class AtomFeedClient : AFeedClient
    {
        XDocument doc;
        string syndicationURl;

        public AtomFeedClient(XDocument doc, string syndicationURl)
        {
            this.doc = doc;
            this.syndicationURl = syndicationURl;
        }

        public override IList<SyndicationItem> Items()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry"))
            {
                items.Add(new SyndicationItem
                {
                    Content = item.Elements().FirstOrDefault(i => i.Name.LocalName == "content").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                    Url = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link").Attribute("href").GetValueOrEmpty(),
                    PublishDate = item.Elements().FirstOrDefault(i => i.Name.LocalName == "published").GetValueOrEmpty().ParseDate(),
                    Title = item.Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                });
            }
            return items.ToList();
        }

        public override SyndicationSummary SyndicationSummary()
        {
            var result = new SyndicationSummary();
            result.Title = doc.Root.Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200);

            var link = doc.Root.Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
            if (!string.IsNullOrWhiteSpace(link.GetValueOrEmpty()))
                result.WebSiteUrl = link.GetValueOrEmpty();
            else
                result.WebSiteUrl = link.Attribute("href").GetValueOrEmpty();


            var selfLink = doc.Root.Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && (i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
            if (selfLink == null)
                result.SyndicationUrl = syndicationURl;
            else if (!string.IsNullOrWhiteSpace(selfLink.GetValueOrEmpty()))
                result.SyndicationUrl = selfLink.GetValueOrEmpty();
            else
                result.SyndicationUrl = selfLink.Attribute("href").GetValueOrEmpty();


            result.PublishDate = doc.Root.Elements().FirstOrDefault(i => i.Name.LocalName == "updated").GetValueOrEmpty().ParseDate();
            result.Description = doc.Root.Elements().FirstOrDefault(i => i.Name.LocalName == "subtitle").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200);

            return result;
        }
    }
}