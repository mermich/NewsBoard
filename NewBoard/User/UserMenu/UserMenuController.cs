using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserMenu
{
    [Area("User")]
    public partial class UserMenuController : BaseController
    {
        UserApi userApi;

        public UserMenuController(UserApi userApi)
        {
            this.userApi = userApi;
        }


        public virtual IActionResult Index()
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


        public virtual ActionResult GetLogin()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserLogin", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }


        public virtual ActionResult GetRegister()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserRegister", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        public virtual ActionResult GetProfile()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserProfile", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }
    }
}