using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NewBoardRestApi.Syndication.Client
{
    public abstract class AFeedClient
    {
        public abstract IList<SyndicationItem> Items();

        public abstract SyndicationSummary SyndicationSummary();


        public DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }

        public string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}