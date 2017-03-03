using SiteParser;
using System;

namespace NewBoardRestApi.DiscoverApi
{
    public class DiscoverArticleVM
    {
        public virtual string Label { get; set; }

        public virtual string Summary { get; set; }

        public virtual string Url { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual DateTime LastUpdatedTime { get; set; }

        public DiscoverArticleVM() { }


        public DiscoverArticleVM(SyndicationItem item)
        {
            Label = item.Title;
            LastUpdatedTime = item.PublishDate;
            PublishDate = item.PublishDate;
            Summary = item.Content;
            Url = item.Url;
        }
    }
}
