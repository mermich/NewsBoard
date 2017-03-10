using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiUtilities
{
    public static class SelectableItemExtentions
    {
        public static SelectableItemt<T> ToSelectableItem<T>(this ISelectable<T> selectable)
            where T : IEquatable<T>
        {
            return ToSelectableItem(selectable, new List<T>());
        }

        public static SelectableItemt<T> ToSelectableItem<T>(this ISelectable<T> selectable, IEnumerable<T> selectedValues)
            where T : IEquatable<T>
        {
            return new SelectableItemt<T>
            {
                IsSelected = selectedValues.Any(ft => ft.Equals(selectable.Value)),
                Label = selectable.Label,
                Value = selectable.Value
            };
        }
    }
}
