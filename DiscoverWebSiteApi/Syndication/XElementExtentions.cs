using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DiscoverWebSiteApi.Syndication
{
    public static class XElementExtentions
    {
        internal static string GetValueOrEmpty(this XElement element)
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
    }
}
