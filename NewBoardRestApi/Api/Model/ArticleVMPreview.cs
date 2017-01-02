using NewBoardRestApi.Syndication;
using System;

namespace NewBoardRestApi.Api.Model
{
    public class ArticleVMPreview
    {
        public virtual string Label { get; set; }

        public virtual string Summary { get; set; }

        public virtual string Url { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual DateTime LastUpdatedTime { get; set; }

        public ArticleVMPreview() { }


        public ArticleVMPreview(SyndicationItem item)
        {
            Label = item.Title;
            LastUpdatedTime = item.PublishDate;
            PublishDate = item.PublishDate;
            Summary = item.Content;
            Url = item.Url;
        }
    }


    public static class ArticleVMPreviewExtentions
    {
        public static ArticleVMPreview ToArticlePreview(this SyndicationItem item)
        {
            return new ArticleVMPreview(item);
        }
    }
}
