﻿using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using System;

namespace NewsBoard.wwwroot.Controls.CascadingData
{
    /// <summary>
    /// Handles everything for the home page
    /// </summary>        
    [Area("Controls")]
    public class CascadingDataController : BaseController
    {
        public IActionResult Index()
        {
            return ReturnView("CascadingDataView", new CascadingDataModel());
        }

        [HttpPost]
        public JsonResult SelectChanged(CascadingDataModel model)
        {
            return new ComposeResult(new ReplaceHtmlResult("#someUpdatedContent", Url.Action("GetTime")), new ErrorMessageResult("drop down value was:" + model.someDropDown));
        }


        public ActionResult GetTime()
        {
            return ReturnView("TimeView", DateTime.Now.Second);
        }
    }
}