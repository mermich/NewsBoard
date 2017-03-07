﻿using DiscoverWebSiteApi.HttpTools;
using DiscoverWebSiteApi.Syndication;
using System;
using System.Linq;

namespace DiscoverWebSiteApi
{
    public class LookupWebSiteApi
    {
        public string GetWebPageContent(string adress)
        {
            return new HttpClientWrapper(adress).GetResponse().ToDocument();
        }

        public string GetPageTitle(string adress)
        {
            var document = new HttpClientWrapper(adress).GetResponse().ToXDocument();
            return document.Elements().FirstOrDefault(i => i.Name.LocalName == "title").Value;
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
            var document = new HttpClientWrapper(adress).GetResponse().ToXDocument();
            var icon = document.Elements().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attribute("type") != null && i.Attribute("type").Value == "image/x-icon").Attribute("href").GetValueOrEmpty();

            if (string.IsNullOrWhiteSpace(icon))
            {
                icon = document.Elements().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attribute("rel") != null && i.Attribute("rel").Value == "shortcut icon").Attribute("href").GetValueOrEmpty();
            }
            else if (string.IsNullOrWhiteSpace(icon))
            {
                icon = document.Elements().FirstOrDefault(i => i.Name.LocalName == "link" && i.Attribute("rel") != null && i.Attribute("rel").Value == "icon").Attribute("href").GetValueOrEmpty();
            }

            return icon;
        }

        public string FindSyndication(string adress)
        {
            Uri baseUrl = new Uri(adress);

            var document = new HttpClientWrapper(adress).GetResponse().ToDocument();
            var rssFeed = document.Split('<').Where(d => (d.Contains("type=\"application/rss+xml\"") || d.Contains("type=\"application/atom+xml\"")) && d.Contains("href=\"")).FirstOrDefault();
            var url = rssFeed.Substring(rssFeed.IndexOf("href=\"")).Substring(6).Split('"')[0];

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
