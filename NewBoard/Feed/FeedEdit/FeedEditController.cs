﻿using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Feed.FeedEdit
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedEditController : BaseController
    {
        FeedApi feedApi;

        public FeedEditController(FeedApi feedApi)
        {
            this.feedApi = feedApi;
        }



        [ResponseCache(Duration = 300)]
        public IActionResult Index(int feedId)
        {
            var model = feedApi.GetFeedEdit(feedId);

            return ReturnView("FeedEditView", model);
        }


        public IActionResult Update(FeedEditVM feed)
        {
            feedApi.SaveFeed(feed);
            return new SuccessMessageResult("Feed Updated");
        }
    }
}