using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace ServerSideSpaTools.TagHelpers
{
    [HtmlTargetElement("div", Attributes = OnlyAuthenticatedAttributeName)]
    [HtmlTargetElement("li", Attributes = OnlyAuthenticatedAttributeName)]
    public class AuthorizedAuthenticatedTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private const string OnlyAuthenticatedAttributeName = "ns-only-authenticated";

        /// <summary>
        /// List of Authorized Roles comma separated.
        /// </summary>
        [HtmlAttributeName(OnlyAuthenticatedAttributeName)]
        public bool OnlyAuthenticated { get; set; } = true;


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var sess = ViewContext.HttpContext.Session;
            var userId = sess.GetInt32("UserId").GetValueOrDefault();

            if (OnlyAuthenticated)
            {
                if (userId == 0)
                {
                    output.SuppressOutput();
                }
            }
            else
            {
                if (userId != 0)
                {
                    output.SuppressOutput();
                }
            }

            base.Process(context, output);
        }
    }
}
