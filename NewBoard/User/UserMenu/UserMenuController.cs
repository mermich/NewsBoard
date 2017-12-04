using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserMenu
{
    [Area("User")]
    public class UserMenuController : BaseController
    {
        UserApi userApi;

        public UserMenuController(UserApi userApi)
        {
            this.userApi = userApi;
        }


        public IActionResult Index()
        {
            if (IsAuthenticated)
            {
                var model = userApi.GetUser(UserId);
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