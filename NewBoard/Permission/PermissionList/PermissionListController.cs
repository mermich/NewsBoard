using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.PermissionApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionList
{
    [Area("Permission")]
    public class PermissionListController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            var api = new PermissionApi(UserId);
            var model = api.GetPermissions();

            return ReturnView("PermissionListView", model);
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetEdit(int PermissionId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionEdit", "Index", new { PermissionId = PermissionId }));
        }

        [ResponseCache(Duration = 300)]
        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionCreate", "Index"));
        }
    }
}