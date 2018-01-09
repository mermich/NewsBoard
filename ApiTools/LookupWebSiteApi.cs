using ApiTools.HttpTools;
using ApiTools.IconSearch;
using ApiTools.SyndicationClient;
using ApiTools.SyndicationSearch;
using ApiTools.WebPage;
using System;
using System.Linq;

namespace ApiTools
{

    public class LookupWebSiteApi
    {
        public string FindPageTitle(HtmlDocumentWrapper doc)
        {
            return doc.GetNodes("title").FirstOrDefault()?.InnerHtml;
        }

        public WebSiteDetails GetWebSiteDetails(Uri uri)
        {
            var hdoc = new HtmlDocumentPageWrapper(uri, new HttpClientWrapper(uri).FetchResponse());

            var details = new WebSiteDetails
            {
                Uri = new RootUriFinder(uri).GetWebSiteRoot(),
                Title = FindPageTitle(hdoc),
                SyndicationUri = new SyndicationSearchStrategy(hdoc).GetFeedSearchOrDefault().GetSyndicationUri(),
                IconUri = new IconSearchStrategy(hdoc).GetIconSearchOrDefault().GetIconUri()
            };


            var xdoc = new XDocumentPageWrapper(details.SyndicationUri, new HttpClientWrapper(details.SyndicationUri).FetchResponse());
            var content = new SyndicationClientStrategy(xdoc).GetSyndicationClientOrDefault().GetSyndicationContent();
            details.Description = content.Description;

            return details;
        }
    }
}
