using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTools.Selectable
{
    public class SelectableItemList<T>
        where T : IEquatable<T>
    {
        public bool IsEditable { get; set; }

        public List<SelectableItem<T>> Items { get; set; } = new List<SelectableItem<T>>();

        public SelectableItemList() : this(new List<SelectableItem<T>>()) { }

        public SelectableItemList(IEnumerable<SelectableItem<T>> items) : this(items, false)
        {
        }

        public SelectableItemList(IEnumerable<SelectableItem<T>> items, bool isEditable)
        {
            Items = items.ToList();
            IsEditable = isEditable;
        }

        public IEnumerable<T> SelectedValues => Items.Where(i => i.IsSelected).Select(i => i.Value);
    }
}
