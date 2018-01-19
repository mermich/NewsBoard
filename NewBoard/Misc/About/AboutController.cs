using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("Misc")]
    public partial class AboutController : BaseController
    {
        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual IActionResult Index()
        {
            return ReturnView("AboutView", null);
        }
    }
}