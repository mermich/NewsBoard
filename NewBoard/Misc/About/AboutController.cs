using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

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