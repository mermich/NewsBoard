using System.Collections.Generic;
using System.Linq;

namespace ApiUtilities
{
    public static class SelectableItemExtentions
    {
        public static SelectableItem ToSelectableItem(this ISelectable selectable, List<string> selectedValues)
        {
            return new SelectableItem
            {
                IsSelected = selectedValues.Any(ft => ft == selectable.Value),
                Label = selectable.Label,
                Value = selectable.Value
            };
        }
    }
}
