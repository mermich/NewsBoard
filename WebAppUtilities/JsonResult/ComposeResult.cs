using System.Collections.Generic;

namespace WebAppUtilities.JsonResult
{
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
