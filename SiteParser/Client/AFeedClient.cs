using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SiteParser.Client
{
    public abstract class AFeedClient
    {
        public abstract IList<SyndicationItem> Items();

        public abstract SyndicationSummary SyndicationSummary();


        internal static DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.Today;
        }

        internal static string GetValueOrEmpty(XElement element)
        {
            try
            {
                return element.Value;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }


        internal static string GetValueOrEmpty(XAttribute element)
        {
            try
            {
                return element.Value;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }

    internal static class AFeedClientExtentions
    {
        internal static string GetValueOrEmpty(this XAttribute element)
        {
            return AFeedClient.GetValueOrEmpty(element);
        }

        internal static string GetValueOrEmpty(this XElement element)
        {
            return AFeedClient.GetValueOrEmpty(element);
        }

        internal static DateTime ParseDate(this string date)
        {
            return AFeedClient.ParseDate(date);
        }
    }
}