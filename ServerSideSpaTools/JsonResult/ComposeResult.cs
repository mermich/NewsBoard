using System.Collections.Generic;

namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Wraps multiple CustomJsonResult into one result.
    /// </summary>
    public class ComposeResult : CustomJsonResult
    {
        public ComposeResult(params CustomJsonResult[] results) : base(new { })
        {
            var build = new List<CustomJsonResult>();
            build.AddRange(results);
            Value = build;
        }
    }
}
