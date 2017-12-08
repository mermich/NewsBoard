using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.User.UserRegister
{
    public class TutorialController : BaseController
    {
        [ResponseCache(Duration = 300)]
        public IActionResult FirstTime()
        {
            return View("FirstTimeView");
        }
    }
}