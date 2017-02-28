using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.BaseApi;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.FeedApi
{
    internal static class FeedEditVMExtentions
    {
        internal static FeedEditVM ToFeedEdit(this Feed feed, IEnumerable<Tag> possibleTags, User currentUser)
        {
            return new FeedEditVM(feed, possibleTags, currentUser);
        }
    }
}
