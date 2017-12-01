using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.PermissionApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Permission.PermissionCreate
{
    [Area("Permission")]
    public class PermissionCreateController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            var api = new PermissionApi(UserId);
            var model = api.GetNewPermissionEditVM();

            return ReturnView("PermissionCreateView", model);
        }

        public ActionResult Create(PermissionVM model)
        {
            var api = new PermissionApi(UserId);
            api.CreatePermission(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Permission", "PermissionList", "Index")),
                new SuccessMessageResult("Permission Created")
                );
        }
    }
}