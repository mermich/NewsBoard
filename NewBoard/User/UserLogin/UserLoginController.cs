using ApiTools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.UserApi;
using NewsBoard.Tools;
using ServerSideSpaTools.JsonResult;
using System.Collections.Generic;
using System.Security.Claims;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("User")]
    public partial class UserLoginController : BaseController
    {
        AuthenticationApi authenticationApi;
        FeedApi feedApi;

        public UserLoginController(AuthenticationApi authenticationApi, FeedApi feedApi)
        {
            this.authenticationApi = authenticationApi;
            this.feedApi = feedApi;
        }


        
        public virtual IActionResult Index()
        {
            var model = authenticationApi.GetNewUserLoginVM();

            return ReturnView("UserLoginView", model);
        }

        public virtual ActionResult Login(UserLoginVM model)
        {
            try
            {
                var user = authenticationApi.Login(model);

                var claims = new List<Claim>
                {
                    // create *required* claims
                    new Claim(ClaimTypes.NameIdentifier, user.Email)
                };

                var permissions = authenticationApi.GetPermissions(user.Id);
                foreach (var permission in permissions)
                {
                    claims.Add(new Claim(ClaimTypes.Role, permission));
                }
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic"));

                HttpContext.SignInAsync("NewsBoardScheme", principal);
                HttpContext.Session.SetInt32("UserId", user.Id);


                var userFeedsAsString = authenticationApi.GetUserFeedsAsString(user.Id);
                HttpContext.Session.SetString("UserFeeds", userFeedsAsString);

                return new ComposeResult(
                    new SuccessMessageResult("Logged"),
                    new ReplaceHtmlResult("#tagCloud", NewsBoardUrlHelper.Action("Tag", "TagCloud", "Index")),
                    new ReplaceHtmlResult("#suggestedFeedListAction", NewsBoardUrlHelper.SuggestedFeedListAction),
                    new ReplaceHtmlResult("#UserMenu", NewsBoardUrlHelper.Action("User", "UserMenu", "Index"), "smallloader"),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("", "Home", "Index")));
            }
            catch (BusinessLogicException ex)
            {
                return new ErrorMessageResult(ex.Message);
            }
        }

        public ActionResult SignOut()
        {
            HttpContext.SignOutAsync("NewsBoardScheme");
            HttpContext.Session.Clear();

            return new ComposeResult(
                    new SuccessMessageResult("Signed Out"),
                    new ReplaceHtmlResult("#tagCloud", NewsBoardUrlHelper.Action("Tag", "TagCloud", "Index")),
                    new ReplaceHtmlResult("#suggestedFeedListAction", NewsBoardUrlHelper.SuggestedFeedListAction),
                    new ReplaceHtmlResult("#UserMenu", NewsBoardUrlHelper.Action("User", "UserMenu", "Index"), "smallloader"),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("", "Home", "Index")));
        }
    }
}