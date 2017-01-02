using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net.Http;

namespace NewsBoard.Tools.TagHelpers
{
    public class BasicCardContext
    {
        public IHtmlContent Content { get; set; }
        public IHtmlContent Action { get; set; }
    }

    [HtmlTargetElement("basicCard")]
    [RestrictChildren("cardContent", "cardAction")]
    public class BasicCardTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        private const string TitleAttributeName = "title";

        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        private const string IdAttributeName = "id";

        [HtmlAttributeName(IdAttributeName)]
        public string Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //inside of a panel
            output.TagName = "div";
            output.Attributes.Add("id", Id);
            output.Attributes.Add("class", "panel panel-default");

            output.TagMode = TagMode.StartTagAndEndTag;


            var modalContext = new BasicCardContext();
            context.Items.Add(typeof(BasicCardTagHelper), modalContext);

            await output.GetChildContentAsync();

            //set panel title
            output.Content.AppendHtml($@"<div class='panel-heading'>{Title}</div>");

            //opens panel body
            output.Content.AppendHtml($@"<div class='panel-body'>");

            if (modalContext.Content != null)
                output.Content.AppendHtml(modalContext.Content);

            if (modalContext.Action != null)
                output.Content.AppendHtml(modalContext.Action);

            //close panel body
            output.Content.AppendHtml(@"</div>");
        }
    }


    [HtmlTargetElement("cardContent", ParentTag = "basicCard")]
    public class CardContentTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();

            var builder = new DefaultTagHelperContent();
            builder.AppendHtml($@"<div class='card-content'>");
            builder.AppendHtml(childContent);
            builder.AppendHtml(@"</div>");

            var modalContext = (BasicCardContext)context.Items[typeof(BasicCardTagHelper)];
            modalContext.Content = builder;

            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("cardAction", ParentTag = "basicCard")]
    public class CardActionTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            
            var builder = new DefaultTagHelperContent();
            builder.AppendHtml($@"<div class='card-actions'>");
            builder.AppendHtml(childContent);
            builder.AppendHtml(@"</div>");

            var modalContext = (BasicCardContext)context.Items[typeof(BasicCardTagHelper)];
            modalContext.Action = builder;


            output.SuppressOutput();
        }
    }
}
