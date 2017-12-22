using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("User")]
    public partial class UserRegisterController : BaseController
    {
        AuthenticationApi authenticationApi;

        public UserRegisterController(AuthenticationApi authenticationApi)
        {
            this.authenticationApi = authenticationApi;
        }


        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index()
        {
            var model = new UserRegisterVM();
            return ReturnView("UserRegisterView", model);
        }

        public virtual ActionResult Register(UserRegisterVM model)
        {
            var userId = authenticationApi.Register(model);

            return new ComposeResult(
                    new SuccessMessageResult("Votre compte est creer, veuilliez vous connecter"),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("User", "UserLogin", "Index")));
        }
    }
}