﻿namespace NewsBoard.Tools.JsonResult
{
    public class OpenUrlResult : CustomJsonResult
    {
        public OpenUrlResult(string url) : base(new { OpenUrl = url })
        {

        }
    }
}