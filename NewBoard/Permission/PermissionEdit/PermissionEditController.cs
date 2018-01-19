using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.PermissionApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionCreate
{
    [Area("Permission")]
    public partial class PermissionEditController : BaseController
    {
        PermissionApi permissionApi;

        public PermissionEditController(PermissionApi permissionApi)
        {
            this.permissionApi = permissionApi;
        }


        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual IActionResult Index(int PermissionId)
        {
            var model = permissionApi.GetPermission(PermissionId);
            
            return ReturnView("PermissionEditView", model);
        }

        public virtual ActionResult Update(PermissionVM model)
        {
            permissionApi.SavePermission(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionList", "Index")),
                new SuccessMessageResult("Permission Updated")
                );
        }
    }
}