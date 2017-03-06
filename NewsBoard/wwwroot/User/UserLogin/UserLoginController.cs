using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using NewBoardRestApi.ArticleApi;
using System.Collections.Generic;
using System.Security.Claims;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("User")]
    public class UserLoginController : BaseController
    {
        public IActionResult Index()
        {
            var model = new AuthenticationApi().GetNewUserLoginVM();

            return ReturnView("UserLoginView", model);
        }

        public ActionResult Login(UserLoginVM model)
        {
            try
            {
                var api = new AuthenticationApi();
                var user = api.Login(model);

                var claims = new List<Claim>();

                // create *required* claims
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Email));

                var permissions = api.GetPermissions(user.Id);
                foreach (var permission in permissions)
                {
                    claims.Add(new Claim(ClaimTypes.Role, permission));
                }
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic"));

                HttpContext.Authentication.SignInAsync("NewsBoardScheme", principal);
                HttpContext.Session.SetInt32("UserId", user.Id);

                return new ComposeResult(
                    new SuccessMessageResult("Logged"),
                    new ReplaceHtmlResult("#UserMenu", NewsBoardUrlHelper.Action("User", "UserMenu", "Index")),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("", "Home", "Index")));
            }
            catch (BusinessLogicException ex)
            {
                return new ErrorMessageResult(ex.Message);
            }
        }

        public ActionResult SignOut()
        {
            HttpContext.Authentication.SignOutAsync("NewsBoardScheme");
            HttpContext.Session.Clear();

            return new ComposeResult(
                    new SuccessMessageResult("Signed Out"),
                    new ReplaceHtmlResult("#UserMenu", NewsBoardUrlHelper.Action("User", "UserMenu", "Index")),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("", "Home", "Index")));
        }
    }
}