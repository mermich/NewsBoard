using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using NewBoardRestApi.ArticleApi;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.User.UserMenu
{
    [Area("User")]
    public class UserMenuController : BaseController
    {
        public IActionResult Index()
        {
            if (IsAuthenticated)
            {
                var model = new UserApi(UserId).GetUser(UserId);
                return ReturnView("UserSignOut", model); 
            }
            else
            {
                return ReturnView("UserSignIn", null);
            }
        }


        public ActionResult GetLogin()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserLogin", "Index"));
        }


        public ActionResult GetRegister()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserRegister", "Index"));
        }

        public ActionResult GetProfile()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserProfile", "Index"));
        }
    }
}