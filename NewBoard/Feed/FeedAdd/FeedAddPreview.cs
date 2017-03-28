using ApiTools;
using ApiTools.Syndication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBoard.wwwroot.Feed.FeedAdd
{
    public class FeedAddPreview
    {
        public WebSiteDetails WebSiteDetails { get; set; }

        public SyndicationContent SyndicationContent { get; set; }
    }
}
