using System;
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
                    Content = item.Elements().First(i => i.Name.LocalName == "content").Value.RemoveHtmlTags().SafeSubtring(200),
                    Url = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
                    PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "published").Value),
                    Title = item.Elements().First(i => i.Name.LocalName == "title").Value.RemoveHtmlTags().SafeSubtring(200),
                });
            }
            return items.ToList();
        }

        public override SyndicationSummary SyndicationSummary()
        {
            var result = new SyndicationSummary();
            result.Title = doc.Root.Elements().First(i => i.Name.LocalName == "title").Value.RemoveHtmlTags().SafeSubtring(200);

            var link = doc.Root.Descendants().First(i => i.Name.LocalName == "link" && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
            if (!string.IsNullOrWhiteSpace(link.Value))
                result.WebSiteUrl = link.Value;
            else
                result.WebSiteUrl = link.Attribute("href").Value;


            var selfLink = doc.Root.Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && (i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
            if (selfLink == null)
                result.SyndicationUrl = syndicationURl;
            else if (!string.IsNullOrWhiteSpace(selfLink.Value))
                result.SyndicationUrl = selfLink.Value;
            else
                result.SyndicationUrl = selfLink.Attribute("href").Value;


            result.PublishDate = DateTime.Parse(doc.Root.Elements().First(i => i.Name.LocalName == "updated").Value);
            result.Description = doc.Root.Elements().First(i => i.Name.LocalName == "subtitle").Value.RemoveHtmlTags().SafeSubtring(200);

            return result;
        }
    }
}