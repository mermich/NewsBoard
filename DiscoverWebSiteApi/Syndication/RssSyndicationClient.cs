using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DiscoverWebSiteApi.Syndication
{
    public class RssSyndicationClient : SyndicationClient
    {
        XDocument doc;
        string syndicationURl;

        public RssSyndicationClient(XDocument doc, string syndicationURl)
        {
            this.doc = doc;
            this.syndicationURl = syndicationURl;
        }

        List<SyndicationItem> Items()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item"))
            {
                items.Add(new SyndicationItem
                {
                    Content = item.Elements().FirstOrDefault(i => i.Name.LocalName == "description").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                    Url = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link").GetValueOrEmpty(),
                    PublishDate = item.Elements().FirstOrDefault(i => i.Name.LocalName == "pubDate").GetValueOrEmpty().ParseDate(),
                    Title = item.Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                });
            }
            return items;
        }

        private SyndicationContent summary;

        public override SyndicationContent SyndicationContent()
        {
            var result = new SyndicationContent();
            result.Title = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().First(i => i.Name.LocalName == "title").Value.RemoveHtmlTags().SafeSubtring(200);

            var link = doc.Root.Descendants().First(i => i.Name.LocalName == "link" && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
            if (!string.IsNullOrWhiteSpace(link.Value))
                result.WebSiteUrl = link.Value;
            else
                result.WebSiteUrl = link.Attribute("href").Value;


            var selfLink = doc.Root.Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml"));
            if (selfLink == null)
                result.SyndicationUrl = syndicationURl;
            else if (!string.IsNullOrWhiteSpace(selfLink.Value))
                result.SyndicationUrl = selfLink.Value;
            else
                result.SyndicationUrl = selfLink.Attribute("href").Value;


            result.PublishDate = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().FirstOrDefault(i => i.Name.LocalName == "lastBuildDate").GetValueOrEmpty().ParseDate();
            result.Description = doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().FirstOrDefault(i => i.Name.LocalName == "description").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200);

            result.Items = Items();

            return result;
        }
    }
}
