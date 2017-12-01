using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Selectable
{
    public class SelectableItemList<T>
        where T : IEquatable<T>
    {
        public List<SelectableItem<T>> Items { get; set; } = new List<SelectableItem<T>>();

        public SelectableItemList() { }

        public SelectableItemList(IEnumerable<SelectableItem<T>> items)
        {
            Items = items.ToList();
        }

        public IEnumerable<T> SelectedValues => Items.Where(i => i.IsSelected).Select(i => i.Value);
    }
}
