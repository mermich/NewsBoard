using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebAppUtilities.TagHelpers
{
    [HtmlTargetElement("button", Attributes = ActionDataTypeName)]
    [HtmlTargetElement("div", Attributes = ActionDataTypeName)]
    [HtmlTargetElement("a", Attributes = ActionDataTypeName)]
    public class ActionTagHelper : TagHelper
    {
        private const string ActionAttributeName = "ns-action-url";
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        private const string ActionDataAttributeName = "ns-action-data";
        [HtmlAttributeName(ActionDataAttributeName)]
        public string ActionData { get; set; }

        private const string ActionDataTypeName = "ns-action-type";
        [HtmlAttributeName(ActionDataTypeName)]
        public string ActionType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("ns-action", Action);
            output.Attributes.Add("ns-action-data", ActionData);
            output.Attributes.Add("ns-action-name", ActionType);

            base.Process(context, output);
        }
    }
}