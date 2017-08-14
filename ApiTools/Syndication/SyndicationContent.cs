using System;
using System.Collections.Generic;

namespace ApiTools.Syndication
{
    /// <summary>
    /// Represents a feed item.
    /// </summary>
    public class SyndicationContent
    {
        public string WebSiteUrl { get; set; } = "";

        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime PublishDate { get; set; } = DateTime.Today;

        public List<SyndicationItem> Items { get; set; } = new List<SyndicationItem>();
    }
}