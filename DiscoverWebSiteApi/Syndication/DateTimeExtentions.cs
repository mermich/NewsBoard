using System;

namespace DiscoverWebSiteApi.Syndication
{
    public static class DateTimeExtentions
    {
        internal static DateTime ParseDate(this string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.Today;
        }
    }
}
