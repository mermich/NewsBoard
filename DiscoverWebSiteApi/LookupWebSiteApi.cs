using DiscoverWebSiteApi.HttpTools;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace DiscoverWebSiteApi
{
    public class LookupWebSiteApi
    {
        public string GetPageTitle(string adress)
        {
            var document = new HttpClientWrapper(adress).GetResponse().ToHtmlDocument();
            return document.GetNodes("title").FirstOrDefault().InnerHtml;
        }


        public string GetWebSiteTitle(string adress)
        {
            var webSiteAdress = GetWebSiteAdress(adress);
            return GetPageTitle(webSiteAdress);
        }

        public WebSiteDetails GetWebSiteDetails(string adress)
        {
            var details = new WebSiteDetails();
            details.WebSiteAdress = GetWebSiteAdress(adress);
            details.WebSiteTitle = GetPageTitle(details.WebSiteAdress);

            details.PageAdress = adress;
            details.PageTitle = GetPageTitle(adress);

            details.IconUrl = GetWebPageIcon(details.WebSiteAdress);
            details.SyndicationAdress = FindSyndication(details.WebSiteAdress);

            return details;
        }

        public string GetWebPageIcon(string adress)
        {
            var document = new HttpClientWrapper(adress).GetResponse().ToHtmlDocument();
            var icon = "";
            HtmlNode element;

            if (string.IsNullOrWhiteSpace(icon))
            {
                element = document.GetNodesByExpression("//link[@type='image/icon']").FirstOrDefault();
                if (element != null)
                    icon = element.Attributes["href"].Value;
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                element = document.GetNodesByExpression("//link[@type='image/x-icon']").FirstOrDefault();
                if (element != null)
                    icon = element.Attributes["href"].Value;
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                element = document.GetNodesByExpression("//link[@rel='shortcut icon']").FirstOrDefault();
                if (element != null)
                    icon = element.Attributes["href"].Value;
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                element = document.GetNodesByExpression("//link[@rel='icon']").FirstOrDefault();
                if (element != null)
                    icon = element.Attributes["href"].Value;
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                icon = "/favicon.ico";
            }

            if (!icon.StartsWith("http://") || !icon.StartsWith("https://"))
            {
                if (!icon.StartsWith("/"))
                    icon = "/" + icon;

                icon = GetWebSiteAdress(adress) + icon;
            }

            return icon;
        }

        public string FindSyndication(string adress)
        {
            Uri baseUrl = new Uri(adress);

            var document = new HttpClientWrapper(adress).GetResponse().ToHtmlDocument();
            var feedNode = document.GetNodesByExpression("//link[@type='application/rss+xml'] | //link[@type='application/atom+xml']").FirstOrDefault();

            var url = feedNode.Attributes["href"].Value;

            if (url.StartsWith("http://"))
                return url;
            if (url.StartsWith("https://"))
                return url;
            else
                return baseUrl.Host + url;
        }

        public string GetWebSiteAdress(string adress)
        {
            Uri baseUrl = new Uri(adress);
            return baseUrl.Scheme + "://" + baseUrl.Host;
        }
    }
}
