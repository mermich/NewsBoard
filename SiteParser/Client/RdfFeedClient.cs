
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SiteParser.Client
{
    public class RdfFeedClient : AFeedClient
    {
        XDocument doc;
        string syndicationURl;

        public RdfFeedClient(XDocument doc, string syndicationURl)
        {
            this.doc = doc;
            this.syndicationURl = syndicationURl;
        }

        public override IList<SyndicationItem> Items()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root.Descendants().Where(i => i.Name.LocalName == "item"))
            {
                items.Add(new SyndicationItem
                {
                    Content = item.Elements().FirstOrDefault(i => i.Name.LocalName == "description").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200),
                    Url = item.Elements().FirstOrDefault(i => i.Name.LocalName == "link").GetValueOrEmpty(),
                    PublishDate = item.Elements().First(i => i.Name.LocalName == "date").GetValueOrEmpty().ParseDate(),
                    Title = item.Elements().FirstOrDefault(i => i.Name.LocalName == "title").GetValueOrEmpty().RemoveHtmlTags().SafeSubtring(200)
                });
            }
            return items;
        }

        public override SyndicationSummary SyndicationSummary()
        {
            throw new NotImplementedException();
        }
    }
}