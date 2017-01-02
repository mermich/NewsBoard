using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.Home
{
    /// <summary>
    /// Handles everything for the home page.
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController():base()
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return ReturnView("IndexView", new HomeModel());
        }
    }
}