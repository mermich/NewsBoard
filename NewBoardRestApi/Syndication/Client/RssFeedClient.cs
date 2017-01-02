using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NewBoardRestApi.Syndication.Client
{
    public class RssFeedClient : AFeedClient
    {
        XDocument doc;
        string syndicationURl;

        public RssFeedClient(XDocument doc, string syndicationURl)
        {
            this.doc = doc;
            this.syndicationURl = syndicationURl;
        }

        public override IList<SyndicationItem> Items()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item"))
            {
                items.Add(new SyndicationItem
                {
                    Content = item.Elements().First(i => i.Name.LocalName == "description").Value.RemoveHtmlTags().SafeSubtring(200),
                    Url = item.Elements().First(i => i.Name.LocalName == "link").Value,
                    PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                    Title = item.Elements().First(i => i.Name.LocalName == "title").Value.RemoveHtmlTags().SafeSubtring(200),
                });
            }
            return items;
        }

        private SyndicationSummary summary;

        public override SyndicationSummary SyndicationSummary()
        {
            if (summary == null)
            {

                summary = new SyndicationSummary();
                summary.Title = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().First(i => i.Name.LocalName == "title").Value.RemoveHtmlTags().SafeSubtring(200);

                var link = doc.Root.Descendants().First(i => i.Name.LocalName == "link" && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
                if (!string.IsNullOrWhiteSpace(link.Value))
                    summary.WebSiteUrl = link.Value;
                else
                    summary.WebSiteUrl = link.Attribute("href").Value;


                var selfLink = doc.Root.Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml"));
                if (selfLink == null)
                    summary.SyndicationUrl = syndicationURl;
                else if (!string.IsNullOrWhiteSpace(selfLink.Value))
                    summary.SyndicationUrl = selfLink.Value;
                else
                    summary.SyndicationUrl = selfLink.Attribute("href").Value;


                summary.PublishDate = DateTime.Parse(doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().First(i => i.Name.LocalName == "lastBuildDate").Value);
                summary.Description = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().First(i => i.Name.LocalName == "description").Value.RemoveHtmlTags().SafeSubtring(200);
            }
            return summary;
        }
    }
}