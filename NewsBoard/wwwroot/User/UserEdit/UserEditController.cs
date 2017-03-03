using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using Microsoft.AspNetCore.Authorization;
using NewBoardRestApi.UserApi;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("User")]
    [Authorize(Roles = "AdminUser")]
    public class UserEditController : BaseController
    {

        public IActionResult Index(int userId)
        {
            var api = new UserApi(UserId);
            var model = api.GetUserEdit(userId);
            
            return ReturnView("GroupEditView", model);
        }

        public ActionResult Update(UserEditVM model)
        {
            var api = new UserApi(UserId);
            api.SaveUser(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserList", "Index")),
                new SuccessMessageResult("User Updated")
                );
        }
    }
}