using ApiTools.HttpTools;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Syndication
{
    public class AtomSyndicationClient : SyndicationClient
    {
        XDocumentWrapper doc;
        string syndicationURl;

        public AtomSyndicationClient(XDocumentWrapper doc, string syndicationURl)
        {
            this.doc = doc;
            this.syndicationURl = syndicationURl;
        }

        List<SyndicationItem> Items()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root().Elements().Where(i => i.Name.LocalName == "entry"))
            {
                var url = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attributes()!= null && i.Attributes().Any(a => a.Name == "rel" && a.Value == "alternate"))?.Attribute("href")?.GetValueOrEmpty();
                if (url == null)
                {
                    url = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link").Attribute("href").GetValueOrEmpty();
                }


                items.Add(new SyndicationItem
                {
                    Content = item.Elements().FirstOrDefault(i => i.Name.LocalName == "content").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                    Url = url,
                    PublishDate = item.Elements().FirstOrDefault(i => i.Name.LocalName == "published" || i.Name.LocalName == "updated").GetValueOrEmpty().ParseDate(),
                    Title = item.Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                });
            }
            return items.ToList();
        }

        public override SyndicationContent SyndicationContent()
        {
            var result = new SyndicationContent
            {
                Title = doc.Root().Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200)
            };

            var link = doc.Root().Descendants().FirstOrDefault(i => i.Name.LocalName == "link"
                && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") || i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));

            if (!string.IsNullOrWhiteSpace(link.GetValueOrEmpty()))
                result.WebSiteUrl = link.GetValueOrEmpty();
            else
                result.WebSiteUrl = link.Attribute("href").GetValueOrEmpty();


            var selfLink = doc.Root().Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && (i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));
            if (selfLink == null)
                result.SyndicationUrl = syndicationURl;
            else if (!string.IsNullOrWhiteSpace(selfLink.GetValueOrEmpty()))
                result.SyndicationUrl = selfLink.GetValueOrEmpty();
            else
                result.SyndicationUrl = selfLink.Attribute("href").GetValueOrEmpty();


            result.PublishDate = doc.Root().Elements().FirstOrDefault(i => i.Name.LocalName == "updated").GetValueOrEmpty().ParseDate().UtcDateTime;
            result.Description = doc.Root().Elements().FirstOrDefault(i => i.Name.LocalName == "subtitle").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200);

            result.Items = Items();


            return result;
        }
    }
}
