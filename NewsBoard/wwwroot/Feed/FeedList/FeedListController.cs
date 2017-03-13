﻿using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.FeedApi.Search;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Feed.FeedList
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedListController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index(FeedVMSearch filter, FeedVMListOptions options)
        {
            var feedRepo = new FeedApi(UserId);
            var model = feedRepo.ListFeed(filter);
            model.Options = options;

            return ReturnView("FeedListView", model);
        }

        public IActionResult Open(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            var feed = feedRepo.GetFeed(feedId);

            // Opens the article and should also update stats.
            return new ComposeResult(
                new OpenNewWindowResult(feed.WebSiteUrl),
                new WarnMessageResult("Ouverture dans une nouvelle fenetre."));
        }


        public IActionResult Subscribe(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.SubscribeFeed(feedId);

            return new ReplaceHtmlResult("#UserSubscription", NewsBoardUrlHelper.Action("Feed", "UserSubscription", "Index"));
        }

        public IActionResult Unsubscribe(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.UnSubscribeFeed(feedId);

            return new ReplaceHtmlResult("#UserSubscription", NewsBoardUrlHelper.Action("Feed", "UserSubscription", "Index"));
        }

        public IActionResult Report(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.ReportFeed(feedId);

            return new SuccessMessageResult("Reported");
        }

        public IActionResult ShowDetails(int feedId)
        {
            var feedRepo = new FeedApi(UserId);
            feedRepo.OpenFeed(feedId);

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedDetails", "Index", new { feedId = feedId }));
        }

        public IActionResult GetEdit(int feedId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Feed", "FeedEdit", "Index", new { feedId = feedId }));
        }
    }
}