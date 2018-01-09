using ApiTools.HttpTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.SyndicationClient
{
    public class RssSyndicationClient : ASyndicationClient
    {
        public RssSyndicationClient(XDocumentPageWrapper doc) : base(doc)
        {
        }

        public override bool IsMatch()
        {
            return doc.Elements().Any(i => i.Name.LocalName == "rss");
        }


        public string GetTitle()
        {
            var channelNode = doc.Root().Descendants().First(i => i.Name.LocalName == "channel");
            var channelNoteTitleValue = channelNode.Elements().First(i => i.Name.LocalName == "title");

            return channelNoteTitleValue.Value.RemoveHtmlTags().SafeSubtring(200);
        }

        public Uri GetWebsiteUri()
        {
            var selfLink = doc.Root().Descendants().First(i => i.Name.LocalName == "link" && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));


            if (selfLink == null)
            {
                throw new BusinessLogicException("Cannot find link to website.");

            }
            else if (!string.IsNullOrWhiteSpace(selfLink.Value))
            {
                return new UriPart(selfLink.Value).ToFullUri(doc.Uri);
            }
            else
            {
                return new UriPart(selfLink.Attribute("href").Value).ToFullUri(doc.Uri);
            }
        }


        public Uri GetSyndicationUri()
        {
            var selfLink = doc.Root().Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml"));

            if (selfLink == null)
            {
                throw new BusinessLogicException("Cannot find link to website.");

            }
            else if (!string.IsNullOrWhiteSpace(selfLink.Value))
            {
                return new UriPart(selfLink.Value).ToFullUri(doc.Uri);
            }
            else
            {
                return new UriPart(selfLink.Attribute("href").Value).ToFullUri(doc.Uri);
            }
        }


        public DateTime GetPublishDate()
        {
            var lastBuildDateNode = doc.Root().Descendants().First(i => i.Name.LocalName == "channel").Elements().FirstOrDefault(i => i.Name.LocalName == "lastBuildDate" || i.Name.LocalName == "pubDate");
            return lastBuildDateNode.GetValueOrEmpty().ParseDate().UtcDateTime;
        }


        public string GetDescription()
        {
            var decriptionNode = doc.Root().Descendants().First(i => i.Name.LocalName == "channel").Elements().FirstOrDefault(i => i.Name.LocalName == "description");
            return decriptionNode.GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200);
        }


        public override SyndicationContent GetSyndicationContent()
        {
            var result = new SyndicationContent
            {
                Title = GetTitle(),
                WebSiteUri = GetWebsiteUri(),
                SyndicationUri = GetSyndicationUri(),
                PublishDate = GetPublishDate(),
                Description = GetDescription(),
                Items = GetItems()
            };

            return result;
        }


        List<SyndicationItem> GetItems()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root().Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item"))
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
    }
}
