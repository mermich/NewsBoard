using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewsBoard.Tools;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.Feed.FeedAdd
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public class FeedAddController : BaseController
    {
        //public IActionResult Index()
        //{
        //    var model = new FeedVMPreview();
        //    return ReturnView("FeedAddView", model);
        //}

        //public IActionResult GetPreview(FeedVMPreview addFeed)
        //{
        //    return new ReplaceHtmlResult("#preview", Url.Action("Preview", "FeedAdd", new { webSiteUrl = addFeed.WebSiteUrl }));
        //}

        //public IActionResult Preview(string webSiteUrl)
        //{
        //    var preview = new FeedApi(UserId).GetPreview(webSiteUrl);

        //    return ReturnView("FeedAddPreviewView", preview);
        //}

        //[HttpPost]
        //public IActionResult CreateSubscription(FeedVMPreview addFeed)
        //{
        //    var feed = new FeedApi(UserId).CreateSubscriptionAndSubScribe(addFeed.SyndicationUrl);

        //    return new ComposeResult(
        //        new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("feed", "FeedList", "Index")),
        //        new SuccessMessageResult("Feed Created")
        //        );
        //}
    }
}