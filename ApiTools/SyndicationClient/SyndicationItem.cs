using System;

namespace ApiTools.SyndicationClient
{
    /// <summary>
    /// Represents a feed item.
    /// </summary>
    public class SyndicationItem
    {
        public string Url { get; set; } = "";


        public string Title { get; set; } = "";


        public string Content { get; set; } = "";


        public DateTimeOffset PublishDate { get; set; } = DateTime.Today;

    }
}