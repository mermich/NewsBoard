using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using ServerSideSpaTools.JsonResult;
using Microsoft.AspNetCore.Authorization;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("User")]
    [Authorize(Roles = "AdminUser")]
    public partial class UserEditController : BaseController
    {
        UserApi userApi;

        public UserEditController(UserApi userApi)
        {
            this.userApi = userApi;
        }


        
        public virtual IActionResult Index(int userId)
        {
            var model = userApi.GetUserEdit(userId);
            
            return ReturnView("UserEditView", model);
        }

        public virtual ActionResult Update(UserEditVM model)
        {
            userApi.SaveUser(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserList", "Index")),
                new SuccessMessageResult("User Updated")
                );
        }
    }
}