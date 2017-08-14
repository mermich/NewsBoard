using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace ServerSideSpaTools
{
    public static class ObjectExtentions
    {
        public static IDictionary<string, object> ToIDictionary(this object anonymousObject)
        {
            return (anonymousObject != null) ? new RouteValueDictionary(anonymousObject) : HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
        }

        public static IDictionary<string, object> MergeObjects(this object defaultObject, object ovverideObject)
        {
            var defaultAttributes = defaultObject.ToIDictionary();

            if (defaultAttributes != null)
            {
                var ovverideAttributes = ovverideObject.ToIDictionary();

                foreach (var item in ovverideAttributes)
                {
                    if (defaultAttributes.ContainsKey(item.Key))
                        defaultAttributes[item.Key] = item.Value;
                    else
                        defaultAttributes.Add(item.Key, item.Value);
                }
            }
            return defaultAttributes;
        }
    }
}
