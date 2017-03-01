using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("div", Attributes = DeniedRolesAttributeName)]
    [HtmlTargetElement("li", Attributes = DeniedRolesAttributeName)]
    public class DeniedRoleTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private const string DeniedRolesAttributeName = "ns-denied-roles";

        /// <summary>
        /// List of Denied Roles comma separated.
        /// </summary>
        [HtmlAttributeName(DeniedRolesAttributeName)]
        public string DeniedRoles { get; set; } = "";        


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var splittedDeniedRoles = DeniedRoles.Split(',');
            var isInDeniedRoles = splittedDeniedRoles.Any(r => ViewContext.HttpContext.Authentication.HttpContext.User.IsInRole(r));

            if (isInDeniedRoles)
            {
                output.SuppressOutput();
            }

            base.Process(context, output);
        }
    }
}
