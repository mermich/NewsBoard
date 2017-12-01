using ApiTools.HttpTools;
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

        public WebSiteDetails GetWebSiteDetails(string adress)
        {
            var uriAdress = new Uri(adress);
            var doc = new HttpClientWrapper(uriAdress).ToHtmlDocument();

            var details = new WebSiteDetails
            {
                Url = GetWebSiteAdress(uriAdress),
                Title = FindPageTitle(doc),
                SyndicationUrl = FindSyndication(uriAdress)?.AbsoluteUri,
                IconUrl = FindWebPageIcon(doc, uriAdress)?.AbsoluteUri
            };


            details.Description = new SyndicationApi().GetSyndication(details.SyndicationUrl).Description;

            // get channel  id :
            // host == "www.youtube.com";
            // var channelId = doc.GetNodesByExpression("//meta[@itemprop='channelId']").First().GetAttributeValue("content");

            // now builds the rss feed from that id:
            // https://www.youtube.com/feeds/videos.xml?channel_id=UCBcRF18a7Qf58cCRy5xuWwQ

            return details;
        }

        public Uri FindWebPageIcon(HtmlDocumentWrapper doc, Uri baseUri)
        {
            var tryChain = new TryChain<string>(() => doc.GetNodesByExpression("//link[@type='image/icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@type='image/x-icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@type='image/x-icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@rel='shortcut icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => doc.GetNodesByExpression("//link[@rel='icon']").FirstOrDefault().GetAttributeValue("href"))
            .ThenTry(() => "/favicon.ico");

            var uri = CombineIfRelative(baseUri, tryChain.Result);

            if (new UriTester(uri).DoesExist())
                return uri;
            else
                return null;
        }

        public Uri CombineIfRelative(Uri baseUri, string toResolve)
        {
            return new Uri(baseUri, toResolve);
        }

        public Uri FindSyndication(Uri address)
        {
            var tryChain = new TryChain<Uri>(() =>
            {
                var document = new HttpClientWrapper(address).ToHtmlDocument();
                var feedNode = document.GetNodesByExpression("//link[@type='application/rss+xml'] | //link[@type='application/atom+xml']").FirstOrDefault();
                return CombineIfRelative(address, feedNode.GetAttributeValue("href"));
            })
            .ThenTry(() =>
            {
                var testUri = new Uri(address, "rss.xml");
                new HttpClientWrapper(testUri).FetchResponse();
                return testUri;
            })
            .ThenTry(() =>
            {
                var testUri = new Uri(address, "atom.xml");
                new HttpClientWrapper(testUri).FetchResponse();
                return testUri;
            })
            .ThenTry(() =>
            {
                var testUri = new Uri(address, "feed.xml");
                new HttpClientWrapper(testUri).FetchResponse();
                return testUri;
            });

            // Could scrapp the entire site to discover any kind of feed.

            return tryChain.Result;
        }


        public string GetWebSiteAdress(Uri adress)
        {
            return adress.Scheme + "://" + adress.Host;
        }
    }
}
