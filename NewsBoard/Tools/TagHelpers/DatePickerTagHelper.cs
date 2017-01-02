using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [OutputElementHint("Should be a datetime")]
    [HtmlTargetElement("date", Attributes = ForAttributeName)]
    public class DatePickerTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

       
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {            
            var value = For.Model;
            if(value is DateTime || value is DateTime?)
            {
                var name = For.Name;
                var fullName = ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
                string progressBarContent = $@"<input icon='fa-flask' value='{value}' class='form-control datepicker input-lg' data-val='true' data-val-date='The field {fullName} must be a date.' data-val-format='yyyy/MM/dd' data-val-language='En' data-val-required='{fullName}' id='{fullName}' name='{fullName}' type='text'>";
                output.Content.AppendHtml(progressBarContent);
                base.Process(context, output);
            }
            else
            {
                throw new ArgumentException("For is not a datetime");
            }
        }
    }
}
