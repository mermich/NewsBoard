using Selectable;
using NewBoardRestApi.DataModel;
using NewBoardRestApi.GroupApi;
using System.Collections.Generic;

namespace NewBoardRestApi.UserApi
{
    public class UserEditVM
    {
        public string Email { get; set; } = "";

        public int Id { get; set; }

        public string  Password { get; set; } = "";

        public SelectableItemList<int> Groups { get; set; }

        public UserEditVM()
        {
        }

        public UserEditVM(List<Group> allGroups)
        {
            Groups = allGroups.ToSelectableItemList();
        }

        public UserEditVM(User user, List<Group> allGroups)
        {
            Email = user.Email;
            Id = user.Id;
            Password = user.Password;
            Groups = allGroups.ToSelectableItemList(user.UserGroups);
        }
    }
}
