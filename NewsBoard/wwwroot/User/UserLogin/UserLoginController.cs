﻿using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

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
            var api = new AuthenticationApi();
            var userId = api.Login(model);

            if (userId == 0)
                return new ErrorMessageResult("Login failed");
            else
                return new ComposeResult(
                    new SuccessMessageResult("Logged"),
                    new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("","Home","Index")));
        }
    }
}