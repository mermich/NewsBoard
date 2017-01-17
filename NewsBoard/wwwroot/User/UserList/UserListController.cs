using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.Api;
using NewsBoard.Tools;
using NewsBoard.Tools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionList
{
    [Area("User")]
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