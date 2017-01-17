using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;
using Microsoft.AspNetCore.Authorization;

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
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserEdit", "Index", new { USerId = userId }));
        }

        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserCreate", "Index"));
        }
    }
}