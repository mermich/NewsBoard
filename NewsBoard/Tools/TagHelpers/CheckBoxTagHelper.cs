using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("checkBox", Attributes = ForAttributeName)]
    public class CheckBoxTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        private const string ForAttributeName = "for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var name = For.Name;
            var fullName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var value = For.Model.ToString();

            string content = $@"<input id='{fullName}' type='checkbox' class='validate'><label for= '{fullName}'>{name}</label>";

            output.Content.AppendHtml(content);

            base.Process(context, output);
        }
    }
}
