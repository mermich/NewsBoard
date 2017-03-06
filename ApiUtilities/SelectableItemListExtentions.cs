using System.Collections.Generic;
using System.Linq;

namespace ApiUtilities
{
    internal static class SelectableItemListExtentions
    {
        internal static SelectableItemList ToSelectableItemList(this IEnumerable<ISelectable> tag, List<string> salectedValues)
        {
            return new SelectableItemList(tag.Select(t => t.ToSelectableItem(salectedValues)));
        }        
    }
}
