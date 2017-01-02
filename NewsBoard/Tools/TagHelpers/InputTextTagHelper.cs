using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("inputText", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InputTextTagHelper : TagHelper
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

            string content = $@"<div class='input-field'><input id='{fullName}' type='text'><label for= '{fullName}'>{name}</label></div>";
                        
            output.Content.AppendHtml(content);
            output.TagMode = TagMode.StartTagAndEndTag;
            base.Process(context, output);
        }
    }
}
