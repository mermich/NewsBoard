using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace NewBoardRestApi.Syndication.Client
{
    public abstract class AFeedClient
    {
        public abstract IList<SyndicationItem> Items();

        public abstract SyndicationSummary SyndicationSummary();


        public static DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.Today;
        }

        public static string GetValueOrEmpty(XElement element)
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


        public static string GetValueOrEmpty(XAttribute element)
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

    public static class AFeedClientExtentions
    {
        public static string GetValueOrEmpty(this XAttribute element)
        {
            return AFeedClient.GetValueOrEmpty(element);
        }

        public static string GetValueOrEmpty(this XElement element)
        {
            return AFeedClient.GetValueOrEmpty(element);
        }

        public static DateTime ParseDate(this string date)
        {
            return AFeedClient.ParseDate(date);
        }
    }
}