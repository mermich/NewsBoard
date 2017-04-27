﻿using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.TagApi;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.FeedApi.Search;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Tag")]
    public class TagCloudController : BaseController
    {

        public IActionResult Index()
        {
            var api = new TagApi(UserId);
            var model = api.GetUsedTags();

            return ReturnView("TagCloudView", model);
        }

        public ActionResult GetBrowseByTag(int id)
        {
            var tagModel = new TagApi(UserId).GetTag(id);
            var filter = new FeedVMSearch();
            filter.Tags.Add(id);

            var options = new FeedVMListOptions { Heading = "Flux du Tag : " + tagModel.Label };

            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.FeedListAction(filter, options));
        }
    }
}