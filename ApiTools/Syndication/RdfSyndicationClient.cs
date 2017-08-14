using ApiTools.HttpTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Syndication
{
    public class RdfSyndicationClient: SyndicationClient
    {
        XDocumentWrapper doc;
        string syndicationURl;

        public RdfSyndicationClient(XDocumentWrapper doc, string syndicationURl)
        {
            this.doc = doc;
            this.syndicationURl = syndicationURl;
        }

        List<SyndicationItem> Items()
        {
            var items = new List<SyndicationItem>();

            foreach (var item in doc.Root().Descendants().Where(i => i.Name.LocalName == "item"))
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

        public override SyndicationContent SyndicationContent()
        {
            throw new NotImplementedException();
        }
    }
}
