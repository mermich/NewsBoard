﻿namespace ServerSideSpaTools.JsonResult
{
    public class OpenNewWindowResult : CustomJsonResult
    {
        public OpenNewWindowResult(string url) : base(new { OpenNewWindow = new { url = url} })
        {

        }
    }
}