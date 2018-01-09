using System;
using System.Collections.Generic;

namespace NewBoardRestApi.DataModel
{
    public class WebSite
    {
        public int Id { get; set; }


        public string IconUrl { get; set; }


        public Uri IconUri => new Uri(IconUrl);


        public string Title { get; set; }


        public List<Feed> Feeds { get; set; } = new List<Feed>();


        public string Url { get; set; }


        public Uri Uri => new Uri(Url);
    }
}
