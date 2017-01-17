using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.Controls.BasicCard
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>    
    [Area("Controls")]
    public class BasicCardController : BaseController
    {

        public ActionResult Index()
        {
            var feedRepo = new FeedApi(UserId);

            var model = new BasicCardModel();
            var item = feedRepo.ListFeed(new FeedListFilterVM()).Feeds.First();

            model.Label = item.Title;
            model.Summary = item.Description;
            model.Uri = item.WebSiteUrl;

            return ReturnView("BasicCardView", model);
        }
    }
}