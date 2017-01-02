using System;

namespace NewBoardRestApi.Syndication
{
    /// <summary>
    /// Represents a feed item.
    /// </summary>
    public class SyndicationSummary
    {
        public string WebSiteUrl { get; set; } = "";

        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime PublishDate { get; set; } = DateTime.Today;        
    }
}