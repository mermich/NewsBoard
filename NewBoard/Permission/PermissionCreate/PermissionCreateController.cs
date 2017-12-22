using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.PermissionApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionCreate
{
    [Area("Permission")]
    public partial class PermissionCreateController : BaseController
    {
        PermissionApi permissionApi;

        public PermissionCreateController(PermissionApi permissionApi)
        {
            this.permissionApi = permissionApi;
        }



        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index()
        {
            var model = permissionApi.GetNewPermissionEditVM();

            return ReturnView("PermissionCreateView", model);
        }

        public virtual ActionResult Create(PermissionVM model)
        {
            permissionApi.CreatePermission(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionList", "Index")),
                new SuccessMessageResult("Permission Created")
                );
        }
    }
}