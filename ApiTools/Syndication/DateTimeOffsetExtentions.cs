using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiTools.Syndication
{
    public static class DateTimeOffsetExtentions
    {
        private static Lazy<List<string>> Formats = new Lazy<List<string>>(() =>
        {
           var formats = new List<string>();

           // two-digit day, four-digit year patterns
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'fffffff zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'ffffff zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'fffff zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'ffff zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'fff zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'ff zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss'.'f zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss zzzz");
           formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss");

           // two-digit day, two-digit year patterns
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'fffffff zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'ffffff zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'fffff zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'ffff zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'fff zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'ff zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss'.'f zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss zzzz");
           formats.Add("ddd',' dd MMM yy HH':'mm':'ss");

           // one-digit day, four-digit year patterns
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'fffffff zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'ffffff zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'fffff zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'ffff zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'fff zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'ff zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss'.'f zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss zzzz");
           formats.Add("ddd',' d MMM yyyy HH':'mm':'ss");

           // two-digit day, two-digit year patterns
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'fffffff zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'ffffff zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'fffff zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'ffff zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'fff zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'ff zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss'.'f zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss zzzz");
           formats.Add("ddd',' d MMM yy HH':'mm':'ss");

           // Fall back patterns
           formats.Add("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK"); // RoundtripDateTimePattern
           formats.Add(DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern);
           formats.Add(DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern);

            //2017-01-06T00:48:21.261Z
            formats.Add("ddd',' dd MMM yyyy HH':'mm':'ss UTC");
            formats.Add("ddd, dd MMM yyyy HH:mm:ss UTC");
            formats.Add("yyyy-MM-HH");
            

            

           return formats;
        });

        internal static DateTimeOffset ParseDate(this string date)
        {
            for (int i = 0; i < Formats.Value.Count; i++)
            {
                if (DateTimeOffset.TryParseExact(date, Formats.Value[i], null, DateTimeStyles.None, out DateTimeOffset result))
                    return result;
            }

            if (DateTimeOffset.TryParse(date, out DateTimeOffset res))
                return res;


            return DateTime.UtcNow;
        }
    }
}