using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("adiv", Attributes = UrlAttributeName )]
    public class AsyncLoaderTagHelper : TagHelper
    {
        private const string UrlAttributeName = "ns-loader-url";

        [HtmlAttributeName(UrlAttributeName)]
        public string Url { get; set; }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("ns-loader-url", Url);

            base.Process(context, output);
        }
    }
}
