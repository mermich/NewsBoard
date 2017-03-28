﻿using NewsBoard.Tools;
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
            return ReturnView("SimpleGetActionView", new SimpleGetActionModel());
        }

        public IActionResult Test()
        {
            return ReturnView("SimpleGetActionView", new SimpleGetActionModel());
        }

        public IActionResult TestwithParam(int id)
        {
            return ReturnView("SimpleGetActionView", new SimpleGetActionModel());
        }
    }
}