using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.BaseApi
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
