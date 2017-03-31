using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
