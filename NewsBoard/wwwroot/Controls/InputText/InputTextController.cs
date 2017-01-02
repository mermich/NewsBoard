using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.Controls.InputTextTest
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>        
    [Area("Controls")]
    public class InputTextController : BaseController
    {
        public IActionResult Index()
        {
            return ReturnView("InputTextView", new InputTextModel());
        }

        [HttpPost]
        public IActionResult Index(InputTextModel model)
        {
            return ReturnView("InputTextView", model);
        }
    }
}