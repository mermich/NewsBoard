using System;
using System.Collections.Generic;

namespace ApiTools.SyndicationClient
{
    /// <summary>
    /// Represents a feed item.
    /// </summary>
    public class SyndicationContent
    {
        public Uri WebSiteUri { get; set; }


        public Uri SyndicationUri { get; set; }


        public string Title { get; set; } = "";


        public string Description { get; set; } = "";


        public DateTime PublishDate { get; set; } = DateTime.Today;


        public List<SyndicationItem> Items { get; set; } = new List<SyndicationItem>();
    }
}