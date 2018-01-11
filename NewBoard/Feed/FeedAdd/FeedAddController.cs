using ApiTools;
using ApiTools.HttpTools;
using ApiTools.SyndicationClient;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;
using System;

namespace NewsBoard.wwwroot.Feed.FeedAdd
{
    /// <summary>
    /// Controller for a single feed
    /// </summary>
    [Area("Feed")]
    public partial class FeedAddController : BaseController
    {
        FeedApi feedApi;

        public FeedAddController(FeedApi feedApi)
        {
            this.feedApi = feedApi;
        }


        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index()
        {
            return ReturnView("FeedAddView", null);
        }

        public virtual IActionResult GetPreview(string urlToDiscover)
        {
            return new ReplaceHtmlResult("#preview", Url.Action("Preview", "FeedAdd", new {  urlToDiscover }));
        }

        public virtual IActionResult Preview(string urlToDiscover)
        {
            var uri = new Uri(urlToDiscover);
            var preview = new LookupWebSiteApi().GetWebSiteDetails(uri);
            var xdoc = new XDocumentPageWrapper(preview.SyndicationUri, new HttpClientWrapper(preview.SyndicationUri).FetchResponse());
            var syndication = new SyndicationClientStrategy(xdoc).GetSyndicationClientOrDefault().GetSyndicationContent();

            var model = new FeedAddPreview
            {
                WebSiteDetails = preview,
                SyndicationContent = syndication
            };
            return ReturnView("FeedAddPreviewView", model);
        }

        [HttpPost]
        public virtual IActionResult CreateSubscription(WebSiteDetails details)
        {
            var feed = feedApi.CreateSubscriptionAndSubScribe(details.Uri, details.SyndicationUri);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.UserFeedListAction),
                new SuccessMessageResult("Feed Created")
                );
        }
    }
}