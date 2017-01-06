using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Api.Model
{
    public class FeedVM
    {
        public int Id { get; set; }

        public string WebSiteUrl { get; set; } = "";

        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int Subscribers { get; set; }


        public ArticleVMList ArticleVMList { get; set; } = new ArticleVMList();

        public FeedVM() { }

        public FeedVM(Feed feed, User currentUser)
        {
            Id = feed.Id;
            WebSiteUrl = feed.WebSiteUrl;
            SyndicationUrl = feed.SyndicationUrl;
            Title = feed.Title;
            Description = feed.Description;
            Subscribers = feed.Subscribers;

            ArticleVMList = feed.Articles.ToArticleList(currentUser);
        }
    }

    public static class FeedVMExtentions
    {
        public static FeedVM ToFeedVM(this Feed feed, User currentUser)
        {
            return new FeedVM(feed, currentUser);
        }
    }
}
