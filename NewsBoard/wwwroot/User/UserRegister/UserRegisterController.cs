using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;
using NewsBoard.Tools;
using NewsBoard.Tools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("User")]
    public class UserRegisterController : BaseController
    {
        public IActionResult Index()
        {
            var model = new UserRegisterVM();
            return ReturnView("UserRegisterView", model);
        }

        public ActionResult Register(UserRegisterVM model)
        {
            var api = new AuthenticationApi();
            var userId = api.Register(model);

            return new ComposeResult(
                    new SuccessMessageResult("Votre compte est creer, veuilliez vous connecter"),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserLogin", "Index")));
        }
    }
}