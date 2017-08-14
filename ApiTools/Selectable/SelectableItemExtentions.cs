using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Selectable
{
    public static class SelectableItemExtentions
    {
        public static SelectableItem<T> ToSelectableItem<T>(this ISelectable<T> selectable)
            where T : IEquatable<T>
        {
            return ToSelectableItem(selectable, new List<T>());
        }

        public static SelectableItem<T> ToSelectableItem<T>(this ISelectable<T> selectable, IEnumerable<T> selectedValues)
            where T : IEquatable<T>
        {
            return new SelectableItem<T>
            {
                IsSelected = selectedValues.Any(ft => ft.Equals(selectable.Value)),
                Label = selectable.Label,
                Value = selectable.Value
            };
        }
    }
}
