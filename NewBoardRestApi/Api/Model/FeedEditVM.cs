using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.Api.Model
{
    public class FeedEditVM
    {
        public int Id { get; set; }

        public string WebSiteUrl { get; set; } = "";

        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int Subscribers { get; set; }

        public SelectableItemList Tags { get; set; }


        public ArticleVMList ArticleVMList { get; set; } = new ArticleVMList();

        public FeedEditVM() { }

        public FeedEditVM(Feed feed, IEnumerable<Tag> possibleTags)
        {
            Id = feed.Id;
            WebSiteUrl = feed.WebSiteUrl;
            SyndicationUrl = feed.SyndicationUrl;
            Title = feed.Title;
            Description = feed.Description;
            Subscribers = feed.Subscribers;

            ArticleVMList = feed.Articles.ToArticleList();

            Tags = possibleTags.ToSelectableItemList(feed.FeedTags);

        }
    }

    public static class FeedEditVMExtentions
    {
        public static FeedEditVM ToFeedEdit(this Feed feed, IEnumerable<Tag> possibleTags)
        {
            return new FeedEditVM(feed, possibleTags);
        }
    }
}
