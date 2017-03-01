using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("adiv", Attributes = UrlAttributeName )]
    public class AsyncLoaderTagHelper : TagHelper
    {
        private const string UrlAttributeName = "ns-loader-url";

        [HtmlAttributeName(UrlAttributeName)]
        public string Url { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("ns-loader-url", Url);

            base.Process(context, output);
        }
    }
}
