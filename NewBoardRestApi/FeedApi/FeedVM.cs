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

        public string IconPath { get; set; } = "";

        public int Subscribers { get; set; }


        public ArticleVMList ArticleVMList { get; set; } = new ArticleVMList();

        public FeedVM() { }

        public FeedVM(Feed feed, User currentUser)
        {
            Id = feed.Id;
            //WebSiteUrl = feed.WebSite.Url;
            //IconPath = feed.WebSite.IconUrl;
            SyndicationUrl = feed.SyndicationUrl;
            Title = feed.Title;
            Description = feed.Description;
            Subscribers = feed.Subscribers;

            if(currentUser!= null)
                IsSubscribed = feed.UserFeeds != null && feed.UserFeeds.Any(uf => uf.UserId == currentUser.Id && uf.IsSubscribed);

            ArticleVMList = feed.Articles.ToArticleList(currentUser);
        }
    }

    internal static class FeedVMExtentions
    {
        internal static FeedVM ToFeedVM(this Feed feed, User currentUser)
        {
            return new FeedVM(feed, currentUser);
        }
    }
}
