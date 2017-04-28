﻿using NewBoardRestApi.DataModel;
using System.Linq;

namespace NewBoardRestApi.ArticleApi
{
    public class ArticleVM
    {
        public int Id { get; private set; }

        public bool IsNew { get; set; }

        public bool IsSubscribed { get; set; }

        public string IsNewClass
        {
            get
            {
                if (IsNew)
                    return "IsNewClass";
                return "";
            }
        }

        public string IconUrl { get; set; } = "";

        public string Label { get; private set; } = "";

        public string Description { get; private set; } = "";

        public string Url { get; private set; } = "";

        public int Subscribers { get; private set; }

        public decimal Score { get; private set; }

        public int FeedId { get; private set; }


        public ArticleVM()
        {
        }

        public ArticleVM(Article article, int userId)
        {
            Id = article.Id;
            Label = article.Label;
            Description = article.Summary;
            Url = article.Url;
            Score = article.Score;
            FeedId = article.FeedId;
            IconUrl = article.Feed.WebSite.IconUrl;

            IsSubscribed = article.Feed.UserFeeds != null && article.Feed.UserFeeds.Any(uf => uf.UserId == userId && uf.IsSubscribed);
            IsNew = article.UserArticles == null || !article.UserArticles.Any(ua => ua.UserId == userId);
        }
    }
}
