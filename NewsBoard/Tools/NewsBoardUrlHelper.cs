using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.FeedApi;
using System.Linq;

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
                new FeedVMSearch { OnlyUserSubscription = false, HideReported = false, MaxItems = 50 },
                new FeedVMListOptions { Heading = "Les Flux" });

        public string UserFeedListAction =>
            FeedListAction(
                new FeedVMSearch { MaxItems = 50 },
                new FeedVMListOptions { Heading = "Mes Flux" });

        public string PopularFeedListAction =>
            FeedListAction(
                new FeedVMSearch { OnlyUserSubscription = false, HideReported = false, MaxItems = 5, OrderBy= null},
                new FeedVMListOptions { Heading = "Les Flux populaires" });


        public string ArticleListAction(ArticleVMSearch filter, ArticleVMListOptions options) => Action("Article", "ArticleList", "Index", filter, options);

        public string AllArticleListAction =>
            ArticleListAction(
                new ArticleVMSearch { OnlyUserSubscription = false, MaxItems = 50 }, 
                new ArticleVMListOptions { Heading = "Tous les Articles" });

        public string UserArticleListAction => ArticleListAction(
            new ArticleVMSearch { OnlyUserSubscription = false, MaxItems = 50 }, 
            new ArticleVMListOptions { Heading = "Articles de mes FLux" });

    }

    public static class UrlHelperExtensions
    {
        public static NewsBoardUrlHelper NewsBoardUrlHelper(this IUrlHelper iUrlHelper)
        {
            return new NewsBoardUrlHelper(iUrlHelper);
        }
    }
}
