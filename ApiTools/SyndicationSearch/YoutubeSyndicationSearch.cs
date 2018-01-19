using ApiTools.HttpTools;
using Jint;
using System;
using System.Linq;

namespace ApiTools.SyndicationSearch
{
    public class YoutubeSyndicationSearch : ASyndicationSearch
    {
        public YoutubeSyndicationSearch(HtmlDocumentPageWrapper doc) : base(doc)
        {
        }

        public Uri BuildFeedUri(string ucid)
        {
            return new Uri("https://www.youtube.com/feeds/videos.xml?channel_id=" + ucid);
        }

        public override bool IsMatch()
        {
            return doc.Uri.Host == "www.youtube.com";
        }

        public override Uri GetSyndicationUri()
        {
            // Instead of a if else, could be build with a strategy...


            if (doc.Uri.ToString().Contains("youtube.com/channel/") || doc.Uri.ToString().Contains("youtube.com/user/"))
            {
                // We are on a user's page, only need to read the content to get the unique id.
                ///  <link rel="canonical" href="https://www.youtube.com/channel/SOME_UCID">
                var playerNode = doc.GetNodesByExpression("//link[@rel='canonical']").FirstOrDefault().GetAttributeValue("href");
                var ucid = playerNode.Split('/').LastOrDefault();

                return BuildFeedUri(ucid);
            }
            else if (doc.Uri.ToString().Contains("youtube.com/watch?"))
            {
                // Find script tag with "var ytplayer = ytplayer" in it:
                var playerNode = doc.GetNodesByExpression("//script").FirstOrDefault(n => n.InnerText.Contains("var ytplayer = ytplayer") && n.InnerText.Contains("ucid"));

                var engine = new Engine();
                var ucid = engine.Execute(
                    "var window = window || {};" +
                    playerNode.InnerText +
                    @"
function log() { return ytplayer.config.args.ucid; }").GetValue("log").Invoke().AsString();

                return BuildFeedUri(ucid);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
