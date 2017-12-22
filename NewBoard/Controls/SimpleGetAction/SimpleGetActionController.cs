using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
namespace NewsBoard.wwwroot.Controls.SimpleGetAction
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>        
    [Area("Controls")]
    public partial class SimpleGetActionController : BaseController
    {
        public virtual IActionResult Index()
        {
            return ReturnView("SimpleGetActionView", new SimpleGetActionModel());
        }

        public virtual IActionResult Test()
        {
            return ReturnView("SimpleGetActionView", new SimpleGetActionModel());
        }

        public virtual IActionResult TestwithParam(int id)
        {
            return ReturnView("SimpleGetActionView", new SimpleGetActionModel());
        }
    }
}