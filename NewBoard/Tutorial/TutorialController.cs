using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.User.UserRegister
{
    public partial class TutorialController : BaseController
    {
        
        public virtual IActionResult FirstTime()
        {
            return View("FirstTimeView");
        }
    }
}