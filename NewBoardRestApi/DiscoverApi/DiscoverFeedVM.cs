﻿using SiteParser.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBoardRestApi.DiscoverApi
{
    public class DiscoverFeedVM
    {
        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public List<DiscoverArticleVM> Articles { get; set; } = new List<DiscoverArticleVM>();

        public DiscoverFeedVM(AFeedClient feedClient)
        {
            SyndicationUrl = feedClient.SyndicationSummary().SyndicationUrl;
            Title = feedClient.SyndicationSummary().Title;
            Description = feedClient.SyndicationSummary().Description;
            Articles.AddRange(feedClient.Items().Select(i => i.ToArticlePreview()));
        }
    }
}