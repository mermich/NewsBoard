using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
namespace NewsBoard.wwwroot.Controls.SimpleGetAction
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>        
    [Area("Controls")]
    public class SimpleGetActionController : BaseController
    {
        public IActionResult Index()
        {
            return PartialView("SimpleGetActionView", new SimpleGetActionModel());
        }

        public IActionResult test()
        {
            return PartialView("SimpleGetActionView", new SimpleGetActionModel());
        }

        public IActionResult testwithParam(int id)
        {
            return PartialView("SimpleGetActionView", new SimpleGetActionModel());
        }
    }
}