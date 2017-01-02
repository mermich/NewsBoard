using System;

namespace NewBoardRestApi.Syndication
{
    /// <summary>
    /// Represents a feed item.
    /// </summary>
    public class SyndicationItem
    {
        public string Url { get; set; } = "";

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public DateTime PublishDate { get; set; } = DateTime.Today;

    }
}