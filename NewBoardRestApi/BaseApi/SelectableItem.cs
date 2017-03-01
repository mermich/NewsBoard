using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.BaseApi
{
    public class SelectableItem
    {
        public int Id { get; set; }

        public bool IsSelected { get; set; }

        public string Label { get; set; }

        public SelectableItem()
        {
        }
    }
}
