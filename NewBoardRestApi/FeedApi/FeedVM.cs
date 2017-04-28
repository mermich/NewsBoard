using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.FeedApi
{
    public class FeedVM
    {
        public int Id { get; set; }

        public bool IsSubscribed { get; set; }

        public string WebSiteUrl { get; set; } = "";

        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public string IconUrl { get; set; } = "";

        public int Subscribers { get; set; }


        public ArticleVMList ArticleVMList { get; set; } = new ArticleVMList();

        public FeedVM() { }

        public FeedVM(Feed feed, int userId)
        {
            Id = feed.Id;
            WebSiteUrl = feed.WebSite.Url;
            IconUrl = feed.WebSite.IconUrl;
            SyndicationUrl = feed.SyndicationUrl;
            Title = feed.Title;
            Description = feed.Description;
            Subscribers = feed.Subscribers;
            IsSubscribed = feed.UserFeeds != null && feed.UserFeeds.Any(uf => uf.UserId == userId && uf.IsSubscribed);

            ArticleVMList = feed.Articles.ToArticleList(userId);
        }
    }
}
