using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.Api.Model;
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



        public string AllFeedListAction =>
            Action("Feed", "FeedList", "Index", 
                new FeedVMListFilter { OnlyUserSubscription = false, HideReported = false, MaxItems = 50 },
                new FeedVMListOptions { Heading = "Les Flux" });

        public string UserFeedListAction =>
            Action("Feed", "FeedList", "Index",
                new FeedVMListFilter { MaxItems = 50 },
                new FeedVMListOptions { Heading = "Mes Flux" });

        public string AllArticleListAction =>
            Action("Article", "ArticleList", "Index", new ArticleVMListFilter { OnlyUserSubscription = false, MaxItems = 50 }, 
                new ArticleVMListOptions { Heading="Tous les Articles" });

        public string UserArticleListAction =>
            Action("Article", "ArticleList", "Index", new ArticleVMListFilter { OnlyUserSubscription = false, MaxItems = 50 },
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
