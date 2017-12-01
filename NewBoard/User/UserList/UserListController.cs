using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionList
{
    [Area("User")]
    [Authorize(Roles = "AdminUser")]
    public class UserListController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            var api = new UserApi(UserId);
            var model = api.ListUsers();

            return ReturnView("UserListView", model);
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetEdit(int userId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserEdit", "Index", new { UserId = userId }));
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserCreate", "Index"));
        }
    }
}