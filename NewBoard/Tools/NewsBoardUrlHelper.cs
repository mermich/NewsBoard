using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.FeedApi.Search;
using System.Linq;
using ServerSideSpaTools;

namespace NewsBoard.Tools
{
    public class NewsBoardUrlHelper
    {
        private IUrlHelper iUrlHelper;

        public NewsBoardUrlHelper(IUrlHelper iUrlHelper)
        {
            this.iUrlHelper = iUrlHelper;
        }


        public string Action(string area, string controller, string action, params object[] values)
        {
            var areaItem = new { area = area };
            var merged = areaItem.MergeObjects(null);

            if (values != null && values.Any())
            {
                foreach (var value in values)
                {
                    merged = merged.MergeObjects(value);
                }
            }
            return iUrlHelper.Action(action, controller, merged);
        }

        public string FeedListAction(FeedVMSearch filter, FeedVMListOptions options) => Action("Feed", "FeedList", "Index", filter, options);

        public string AllFeedListAction =>
            FeedListAction(
                new FeedVMSearch { SubscriptionFilter = SubscriptionFilter.All, HideReported = false, MaxItems = 50 },
                new FeedVMListOptions { Heading = "Les Flux" });

        public string UserFeedListAction =>
            FeedListAction(
                new FeedVMSearch { MaxItems = 50 },
                new FeedVMListOptions { Heading = "Mes Flux" });

        public string SuggestedFeedListAction =>
            FeedListAction(
                new FeedVMSearch { SubscriptionFilter = SubscriptionFilter.OnlyUnSubscribbed, HideReported = true, MaxItems = 5, ShouldPickRandomItems = true },
                new FeedVMListOptions { Heading = "Les Flux suggeres" });


        public string ArticleListAction(ArticleVMSearch filter, ArticleVMListOptions options) => Action("Article", "ArticleList", "Index", filter, options);

        public string AllArticleListAction =>
            ArticleListAction(
                new ArticleVMSearch { SubscriptionFilter = SubscriptionFilter.All, MaxItems = 50 },
                new ArticleVMListOptions { Heading = "Tous les Articles" });

        public string UserArticleListAction => ArticleListAction(
            new ArticleVMSearch { SubscriptionFilter = SubscriptionFilter.OnlySubscribbed, MaxItems = 50 },
            new ArticleVMListOptions { Heading = "Articles de mes Flux" });


    }
}
