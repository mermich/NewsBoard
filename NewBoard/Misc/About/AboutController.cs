using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Misc")]
    public class AboutController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            return ReturnView("AboutView", null);
        }
    }
}