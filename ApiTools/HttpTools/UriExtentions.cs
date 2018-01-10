using System;

namespace ApiTools.WebPage
{
    public static class UriExtentions
    {
        public static Uri GetWebSiteRoot(this Uri uri)
        {
            return new Uri(uri.Scheme + "://" + uri.Host);
        }
    }
}
