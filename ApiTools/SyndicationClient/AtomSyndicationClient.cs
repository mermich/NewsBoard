using ApiTools.HttpTools;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.SyndicationClient
{
    public class AtomSyndicationClient : ASyndicationClient
    {
        public AtomSyndicationClient(XDocumentPageWrapper doc) : base(doc)
        {
        }

        public override SyndicationContent GetSyndicationContent()
        {
            var result = new SyndicationContent
            {
                Title = doc.Root().Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200)
            };

            var link = doc.Root().Descendants().FirstOrDefault(i => i.Name.LocalName == "link"
                && !(i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") || i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));


            var linkValue = link.GetValueOrEmpty();

            if (!string.IsNullOrWhiteSpace(linkValue))
            {
                result.WebSiteUri = new UriPart(linkValue).ToFullUri(doc.Uri);
            }
            else
            {
                var linkHrefValue = link.Attribute("href").GetValueOrEmpty();
                if (!string.IsNullOrWhiteSpace(linkHrefValue))
                {
                    result.WebSiteUri = new UriPart(linkHrefValue).ToFullUri(doc.Uri);
                }
            }



            var selfLink = doc.Root().Descendants().FirstOrDefault(i => i.Name.LocalName == "link" && (i.Attributes().Any(a => a.Name == "rel" && a.Value == "self") && i.Attributes().Any(a => a.Name == "type" && a.Value == "application/rss+xml")));


            if (selfLink == null)
            {
                result.SyndicationUri = doc.Uri;
            }
            else if (!string.IsNullOrWhiteSpace(selfLink.GetValueOrEmpty()))
            {
                result.SyndicationUri = new UriPart(selfLink.GetValueOrEmpty()).ToFullUri(doc.Uri);
            }
            else
            {
                result.SyndicationUri = new UriPart(selfLink.Attribute("href").GetValueOrEmpty()).ToFullUri(doc.Uri);
            }



            result.PublishDate = doc.Root().Elements().FirstOrDefault(i => i.Name.LocalName == "updated").GetValueOrEmpty().ParseDate().UtcDateTime;
            result.Description = doc.Root().Elements().FirstOrDefault(i => i.Name.LocalName == "subtitle").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200);

            result.Items = GetItems();


            return result;
        }


        List<SyndicationItem> GetItems()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root().Elements().Where(i => i.Name.LocalName == "entry"))
            {
                var url = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attributes() != null && i.Attributes().Any(a => a.Name == "rel" && a.Value == "alternate"))?.Attribute("href")?.GetValueOrEmpty();
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

        public override bool IsMatch()
        {
            return doc.Elements().Any(i => i.Name.LocalName == "feed");
        }
    }
}
