using ApiUtilities;
using NewBoardRestApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace NewBoardRestApi.GroupApi
{
    internal static class GroupVMListExtentions
    {
        internal static GroupVMList ToGroupVMList(this IEnumerable<Group> items)
        {
            return new GroupVMList(items.Select(i => i.ToGroup()));
        }

        public static SelectableItemList<int>  ToSelectableItemList(this IEnumerable<UserGroup> groups)
        {
            return new SelectableItemList<int>();
        }


        public static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Group> groups)
        {
            return new SelectableItemList<int>();
        }

        public static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Group> groups, object plop)
        {
            return new SelectableItemList<int>();
        }


        public static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Permission> groups)
        {
            return new SelectableItemList<int>();
        }

        public static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Permission> groups, object plop)
        {
            return new SelectableItemList<int>();
        }


        public static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Tag> groups)
        {
            return new SelectableItemList<int>();
        }

        public static SelectableItemList<int> ToSelectableItemList(this IEnumerable<Tag> groups, object plop)
        {
            return new SelectableItemList<int>();
        }

        
    }
}
