using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewsBoard.wwwroot.Permission.PermissionList
{
    [Area("User")]
    //[Authorize(Roles = "AdminUser")]
    public partial class UserListController : BaseController
    {
        UserApi userApi;

        public UserListController(UserApi userApi)
        {
            this.userApi = userApi;
        }


        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index()
        {
            var model = userApi.ListUsers();

            return ReturnView("UserListView", model);
        }

        [ResponseCache(Duration = 300)]
        public virtual ActionResult GetEdit(int userId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserEdit", "Index", new {  userId } ));
        }

        [ResponseCache(Duration = 300)]
        public virtual ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserCreate", "Index"));
        }
    }
}