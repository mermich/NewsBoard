using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiTools.SyndicationClient
{
    public static class DateTimeOffsetExtentions
    {
        private static Lazy<List<string>> Formats = new Lazy<List<string>>(() =>
        {
            var formats = new List<string>
            {
                // two-digit day, four-digit year patterns
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'fffffff zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'ffffff zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'fffff zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'ffff zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'fff zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'ff zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss'.'f zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss zzzz",
                "ddd',' dd MMM yyyy HH':'mm':'ss",

                // two-digit day, two-digit year patterns
                "ddd',' dd MMM yy HH':'mm':'ss'.'fffffff zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss'.'ffffff zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss'.'fffff zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss'.'ffff zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss'.'fff zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss'.'ff zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss'.'f zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss zzzz",
                "ddd',' dd MMM yy HH':'mm':'ss",

                // one-digit day, four-digit year patterns
                "ddd',' d MMM yyyy HH':'mm':'ss'.'fffffff zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss'.'ffffff zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss'.'fffff zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss'.'ffff zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss'.'fff zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss'.'ff zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss'.'f zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss zzzz",
                "ddd',' d MMM yyyy HH':'mm':'ss",

                // two-digit day, two-digit year patterns
                "ddd',' d MMM yy HH':'mm':'ss'.'fffffff zzzz",
                "ddd',' d MMM yy HH':'mm':'ss'.'ffffff zzzz",
                "ddd',' d MMM yy HH':'mm':'ss'.'fffff zzzz",
                "ddd',' d MMM yy HH':'mm':'ss'.'ffff zzzz",
                "ddd',' d MMM yy HH':'mm':'ss'.'fff zzzz",
                "ddd',' d MMM yy HH':'mm':'ss'.'ff zzzz",
                "ddd',' d MMM yy HH':'mm':'ss'.'f zzzz",
                "ddd',' d MMM yy HH':'mm':'ss zzzz",
                "ddd',' d MMM yy HH':'mm':'ss",

                // Fall back patterns
                "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK", // RoundtripDateTimePattern
                DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern,
                DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern,

                //2017-01-06T00:48:21.261Z
                "ddd',' dd MMM yyyy HH':'mm':'ss UTC",
                "ddd, dd MMM yyyy HH:mm:ss UTC",
                "yyyy-MM-HH"
            };


            return formats;
        });

        internal static DateTimeOffset ParseDate(this string date)
        {
            DateTimeOffset res = DateTime.UtcNow;

            for (int i = 0; i < Formats.Value.Count; i++)
            {
                if (DateTimeOffset.TryParseExact(date, Formats.Value[i], null, DateTimeStyles.None, out DateTimeOffset resultExact))
                {
                    res = resultExact;
                }
            }

            if (DateTimeOffset.TryParse(date, out DateTimeOffset result))
            {
                res = result;
            }

            return res;
        }
    }
}