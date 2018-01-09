using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ServerSideSpaTools.TagHelpers
{
    [HtmlTargetElement("button", Attributes = ActionDataTypeName)]
    [HtmlTargetElement("div", Attributes = ActionDataTypeName)]
    [HtmlTargetElement("a", Attributes = ActionDataTypeName)]
    public class ActionTagHelper : TagHelper
    {
        private const string ActionAttributeName = "href";


        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        private const string ActionDataTypeName = "ns-action-type";


        [HtmlAttributeName(ActionDataTypeName)]
        public string ActionType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("href", Action);
            output.Attributes.Add("ns-action-type", ActionType);

            base.Process(context, output);
        }
    }
}