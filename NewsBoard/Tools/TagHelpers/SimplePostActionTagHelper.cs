using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("simplePostAction", Attributes = ActionAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class SimplePostActionTagHelper : TagHelper
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
            output.Attributes.Add("name", "simplePostAction");
            
            string content = $@"{Label}<span class='glyphicon glyphicon-ok-circle' aria-hidden='true' />";
            output.Content.AppendHtml(content);

            base.Process(context, output);
        }
    }
}