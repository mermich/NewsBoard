﻿using NewBoardRestApi.ArticleApi;
using SiteParser.Client;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.FeedApi
{
    public class FeedVMPreview
    {
        public string WebSiteUrl { get; set; } = "";

        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";


        public List<ArticleVMPreview> ArticlePreviews { get; set; } = new List<ArticleVMPreview>();

        public FeedVMPreview() { }

        public FeedVMPreview(AFeedClient feedClient)
        {
            WebSiteUrl = feedClient.SyndicationSummary().WebSiteUrl;
            SyndicationUrl = feedClient.SyndicationSummary().SyndicationUrl;
            Title = feedClient.SyndicationSummary().Title;
            Description = feedClient.SyndicationSummary().Description;

            ArticlePreviews.AddRange(feedClient.Items().Select(i => i.ToArticlePreview()));
        }
    }
}
