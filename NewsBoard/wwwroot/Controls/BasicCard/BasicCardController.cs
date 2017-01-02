using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NewBoardRestApi.Api;

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
            var feedRepo = new FeedApi(HttpContext.Session.Id);

            var model = new BasicCardModel();
            var item = feedRepo.ListFeed().Feeds.First();

            model.Label = item.Title;
            model.Summary = item.Description;
            model.Uri = item.WebSiteUrl;

            return ReturnView("BasicCardView", model);
        }
    }
}