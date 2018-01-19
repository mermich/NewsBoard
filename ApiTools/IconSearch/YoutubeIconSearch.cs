using ApiTools.HttpTools;
using System;
using System.Linq;

namespace ApiTools.IconSearch
{
    public class YoutubeIconSearch : AIconSearch
    {
        public YoutubeIconSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public override bool IsMatch()
        {
            return doc.Uri.Host == "www.youtube.com";
        }

        public override Uri GetIconUri()
        {
            // Instead of a if else, could be build with a strategy...

            if (doc.Uri.ToString().Contains("youtube.com/channel/") || doc.Uri.ToString().Contains("youtube.com/user/"))
            {
                var iconNode = doc.GetNodesByExpression("//img[@class='appbar-nav-avatar']").FirstOrDefault();
                return new Uri(iconNode.GetAttribute("src").Value);
            }
            else if (doc.Uri.ToString().Contains("youtube.com/watch?"))
            {
                var iconNode = doc.GetNodesByExpression("//img[@data-thumb]").FirstOrDefault();
                return new Uri(iconNode.GetAttribute("data-thumb").Value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
