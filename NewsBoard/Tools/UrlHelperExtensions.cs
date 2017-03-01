using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.FeedApi;
using System.Linq;

namespace NewsBoard.Tools
{
    public static class UrlHelperExtensions
    {
        public static NewsBoardUrlHelper NewsBoardUrlHelper(this IUrlHelper iUrlHelper)
        {
            return new NewsBoardUrlHelper(iUrlHelper);
        }
    }
}
