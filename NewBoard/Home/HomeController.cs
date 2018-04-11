using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using ServerSideSpaTools.JsonResult;
using NewBoardRestApi.FeedApi;

namespace NewsBoard.wwwroot.Home
{
    /// <summary>
    /// Handles everything for the home page.
    /// </summary>
    public partial class HomeController : BaseController
    {
        FeedApi feedApi;

        public HomeController(FeedApi feedApi) : base()
        {
            this.feedApi = feedApi;
        }

        public virtual IActionResult Index()
        {
            return ReturnView("IndexView", new HomeModel());
        }

        
        public virtual IActionResult GetUserArticles()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserArticleListAction).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        public virtual IActionResult GetArticleSearch()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllArticleListAction).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetUserFeeds()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().UserFeedListAction).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetAllFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().AllFeedListAction).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }


        
        public virtual IActionResult GetAddFeed()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Feed", "FeedAdd", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetAboutPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Misc", "About", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetTagListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Tag", "TagList", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetGroupListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Group", "GroupList", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetPermissionListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("Permission", "PermissionList", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetUserListPage()
        {
            return new ReplaceMainHtmlResult(Url.NewsBoardUrlHelper().Action("User", "UserList", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual IActionResult GetRefreshFeeds()
        {
            feedApi.RefreshFeedsArticles();
            return new WarnMessageResult("Rafraichissement en cours");
        }
    }
}