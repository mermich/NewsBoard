using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("abutton", Attributes = ActionDataTypeName)]
    [HtmlTargetElement("adiv", Attributes = ActionDataTypeName)]
    [HtmlTargetElement("aa", Attributes = ActionDataTypeName)]
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
        public ActionTypeEnum ActionType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("ns-action", Action);
            output.Attributes.Add("ns-action-data", ActionData);
            output.Attributes.Add("ns-action-name", ActionType);

            base.Process(context, output);
        }
    }

    public enum ActionTypeEnum
    {
        simpleGetAction,
        simplePostAction
    }
}