using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsBoard.Tools.TagHelpers
{
    [HtmlTargetElement("asyncLoader", Attributes = UrlAttributeName )]
    public class AsyncLoaderTagHelper : TagHelper
    {
        private const string UrlAttributeName = "url";

        [HtmlAttributeName(UrlAttributeName)]
        public string Url { get; set; }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await output.GetChildContentAsync();

            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("url", Url);
            base.Process(context, output);
        }
    }
}
