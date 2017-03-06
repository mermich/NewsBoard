using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using NewBoardRestApi.ArticleApi;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionList
{
    [Area("User")]
    [Authorize(Roles = "AdminUser")]
    public class UserListController : BaseController
    {

        public IActionResult Index()
        {
            var api = new UserApi(UserId);
            var model = api.ListUsers();

            return ReturnView("UserListView", model);
        }

        public ActionResult GetEdit(int userId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserEdit", "Index", new { UserId = userId }));
        }

        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserCreate", "Index"));
        }
    }
}