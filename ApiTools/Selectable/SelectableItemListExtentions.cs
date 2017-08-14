using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Selectable
{
    internal static class SelectableItemListExtentions
    {

        public static SelectableItemList<T> ToSelectableItemList<T>(this IEnumerable<ISelectable<T>> items, List<T> salectedValues)
            where T : IEquatable<T>
        {
            return new SelectableItemList<T>(items.Select(t => t.ToSelectableItem(salectedValues)));
        }
    }
}
