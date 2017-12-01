using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.PermissionApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionCreate
{
    [Area("Permission")]
    public class PermissionEditController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index(int PermissionId)
        {
            var api = new PermissionApi(UserId);
            var model = api.GetPermission(PermissionId);
            
            return ReturnView("PermissionEditView", model);
        }

        public ActionResult Update(PermissionVM model)
        {
            var api = new PermissionApi(UserId);
            api.SavePermission(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionList", "Index")),
                new SuccessMessageResult("Permission Updated")
                );
        }
    }
}