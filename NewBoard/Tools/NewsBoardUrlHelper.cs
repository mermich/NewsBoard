﻿using Microsoft.AspNetCore.Mvc;
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
            var areaItem = new { area };
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
                new FeedVMListOptions("Les Flux"));

        public string UserFeedListAction =>
            FeedListAction(
                new FeedVMSearch { MaxItems = 50 },
                new FeedVMListOptions("Mes Flux", "Aucuns flux a afficher, il faudrait peut etre souscrire a des flux."));

        public string SuggestedFeedListAction =>
            FeedListAction(
                new FeedVMSearch { SubscriptionFilter = SubscriptionFilter.OnlyUnSubscribbed, HideReported = true, MaxItems = 5, ShouldPickRandomItems = true, OrderBy = FeedListOrderBy.Subscriptions },
                new FeedVMListOptions("Les Flux suggeres"));


        public string ArticleListAction(ArticleVMSearch filter, ArticleVMListOptions options) => Action("Article", "ArticleList", "Index", filter, options);

        public string AllArticleListAction =>
            ArticleListAction(
                new ArticleVMSearch { SubscriptionFilter = SubscriptionFilter.All, MaxItems = 50 },
                new ArticleVMListOptions("Tous les Articles"));

        public string UserArticleListAction => ArticleListAction(
            new ArticleVMSearch { SubscriptionFilter = SubscriptionFilter.OnlySubscribbed, MaxItems = 50 },
            new ArticleVMListOptions("Articles de mes Flux", "Aucuns resultats a afficher, il faudrait peut etre souscrire a plus de flux."));

        public string ArticleForFeedListAction(int feedId) => ArticleListAction(
          ArticleVMSearch.BuildSerachByFeedId(feedId),
          new ArticleVMListOptions("Articles du flux"));

    }
}
