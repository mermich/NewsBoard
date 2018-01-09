using System;

namespace ApiTools.WebPage
{
    public class RootUriFinder
    {
        private Uri uri;


        public RootUriFinder(Uri uri)
        {
            this.uri = uri;
        }

        public Uri GetWebSiteRoot()
        {
            return new Uri(uri.Scheme + "://" + uri.Host);
        }
    }
}
