using Selectable;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using NewBoardRestApi.GroupApi;

namespace NewBoardRestApi.FeedApi
{
    public class FeedEditVM
    {
        public int Id { get; set; }

        public string WebSiteUrl { get; set; } = "";

        public string IconUrl { get; set; } = "";
        
        public string SyndicationUrl { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int Subscribers { get; set; }

        public SelectableItemList<int> Tags { get; set; } = new SelectableItemList<int>();


        public ArticleVMList ArticleVMList { get; set; } = new ArticleVMList();

        public FeedEditVM() { }

        public FeedEditVM(Feed feed, IEnumerable<Tag> possibleTags, User currentUser)
        {
            Id = feed.Id;
            //WebSiteUrl = feed.WebSite.Url;
            //IconUrl = feed.WebSite.IconUrl;
            SyndicationUrl = feed.SyndicationUrl;
            Title = feed.Title;
            Description = feed.Description;
            Subscribers = feed.Subscribers;

            ArticleVMList = feed.Articles.ToArticleList(currentUser);

            Tags = possibleTags.ToSelectableItemList(feed.FeedTags);

        }
    }
}
