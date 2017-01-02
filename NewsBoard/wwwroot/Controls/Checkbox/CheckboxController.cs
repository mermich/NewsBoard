using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;

namespace NewsBoard.wwwroot.Controls.CheckboxTest
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>   
    [Area("Controls")]
    public class CheckboxController : BaseController
    {
        public IActionResult Index()
        {
            return this.ReturnView("CheckboxView", new CheckboxModel());
        }

        [HttpPost]
        public IActionResult Index(CheckboxModel model)
        {
            return this.ReturnView("CheckboxView", model);
        }
    }
}