using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace WebAppUtilities.TagHelpers
{
    [HtmlTargetElement("div", Attributes = AuthorizedRolesAttributeName)]
    [HtmlTargetElement("li", Attributes = AuthorizedRolesAttributeName)]
    public class AuthorizedRolesTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private const string AuthorizedRolesAttributeName = "ns-authorized-roles";

        /// <summary>
        /// List of Authorized Roles comma separated.
        /// </summary>
        [HtmlAttributeName(AuthorizedRolesAttributeName)]
        public string AuthorizedRoles { get; set; } = "";


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var splittedAuthorizedRoles = AuthorizedRoles.Split(',');
            var isInAuthorizedRoles = splittedAuthorizedRoles.Any(r => ViewContext.HttpContext.Authentication.HttpContext.User.IsInRole(r));

            if (!isInAuthorizedRoles)
            {
                output.SuppressOutput();
            }

            base.Process(context, output);
        }
    }
}
