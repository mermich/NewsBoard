using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("navigateGetAction", Attributes = ActionAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class NavigateGetActionTagHelper : TagHelper
    {
        private const string ActionAttributeName = "action";


        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        private const string ActionDataAttributeName = "actionData";


        [HtmlAttributeName(ActionDataAttributeName)]
        public string ActionData { get; set; }

        private const string LabelAttributeName = "label";


        [HtmlAttributeName(LabelAttributeName)]
        public string Label { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "button";
            output.Attributes.Add("class", "btn btn-small");
            output.Attributes.Add("action", Action);
            output.Attributes.Add("actionData", ActionData);
            output.Attributes.Add("type", "button");
            output.Attributes.Add("name", "simpleGetAction");

            string content = $@"{Label}<i class='material-icons right small'>send</i>";
            output.Content.AppendHtml(content);

            base.Process(context, output);
        }
    }
}