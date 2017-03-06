using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebAppUtilities.TagHelpers
{
    [HtmlTargetElement("div", Attributes = IsVisibleAttributeName)]
    [HtmlTargetElement("li", Attributes = IsVisibleAttributeName)]
    public class VisibilityTagHelper : TagHelper
    {
        private const string IsVisibleAttributeName = "ns-is-visible";
        [HtmlAttributeName(IsVisibleAttributeName)]
        public bool IsVisible { get; set; } = true;

        // You only need one of these Process methods, but just showing the sync and async versions
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!IsVisible)
                output.SuppressOutput();

            base.Process(context, output);
        }
    }
}
