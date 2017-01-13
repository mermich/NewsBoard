using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Misc")]
    public class AboutController : BaseController
    {

        public IActionResult Index()
        {
            return ReturnView("AboutView", null);
        }
    }
}