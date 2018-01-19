using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

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


        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual IActionResult Index()
        {
            var model = userApi.ListUsers();

            return ReturnView("UserListView", model);
        }

        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual ActionResult GetEdit(int userId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserEdit", "Index", new {  userId } )).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserCreate", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }
    }
}