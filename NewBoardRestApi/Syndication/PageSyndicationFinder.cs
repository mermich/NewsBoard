using System.Linq;

namespace NewBoardRestApi.Syndication
{
    public class PageSyndicationFinder
    {
        string document;

        public PageSyndicationFinder(string document)
        {
            this.document = document;

        }

        public string GetSyndicationURl(string baseUrl)
        {
            var rssFeed = document.Split('<').Where(d => d.Contains("type=\"application/rss+xml\"") && d.Contains("href=\"")).FirstOrDefault();
            var url = rssFeed.Substring(rssFeed.IndexOf("href=\"")).Substring(6).Split('"')[0];

            if (url.StartsWith("http://"))
                return url;
            if (url.StartsWith("https://"))
                return url;
            else
                return baseUrl + url;
        }
    }
}
