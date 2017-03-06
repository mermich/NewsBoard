using System.Collections.Generic;
using System.Linq;

namespace ApiUtilities
{
    public class SelectableItemList
    {
        public List<SelectableItem> Items { get; set; } = new List<SelectableItem>();

        public SelectableItemList() { }

        public SelectableItemList(IEnumerable<SelectableItem> items)
        {
            Items = items.ToList();
        }
    }
}
