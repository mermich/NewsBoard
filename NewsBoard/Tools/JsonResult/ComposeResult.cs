using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NewsBoard.Tools.JsonResult
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
