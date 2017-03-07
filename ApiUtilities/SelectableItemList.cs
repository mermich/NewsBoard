using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiUtilities
{
    public class SelectableItemList<T>
        where T : IEquatable<T>
    {
        public List<SelectableItemt<T>> Items { get; set; } = new List<SelectableItemt<T>>();

        public SelectableItemList() { }

        public SelectableItemList(IEnumerable<SelectableItemt<T>> items)
        {
            Items = items.ToList();
        }
    }
}
