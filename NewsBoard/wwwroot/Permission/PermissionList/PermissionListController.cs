using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;
using Microsoft.AspNetCore.Authorization;

namespace NewsBoard.wwwroot.Permission.PermissionList
{
    [Area("Permission")]
    public class PermissionListController : BaseController
    {

        public IActionResult Index()
        {
            var api = new PermissionApi(UserId);
            var model =api.GetPermissions();
            
            return ReturnView("PermissionListView", model);
        }

        public ActionResult GetEdit(int PermissionId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionEdit", "Index", new { PermissionId = PermissionId }));
        }

        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionCreate", "Index"));
        }
    }
}