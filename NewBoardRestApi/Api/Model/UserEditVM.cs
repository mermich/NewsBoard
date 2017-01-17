﻿using NewBoardRestApi.DataModel;
using System.Collections.Generic;

namespace NewBoardRestApi.Api.Model
{
    public class UserEditVM
    {
        public string Email { get; set; }

        public int Id { get; set; }

        public string  Password { get; set; }
        
        public SelectableItemList Groups { get; set; }

        public UserEditVM()
        {
        }

        public UserEditVM(List<Group> allGroups)
        {
            Groups = allGroups.ToSelectableItemList(new List<UserGroup>());
        }

        public UserEditVM(User user, List<Group> allGroups)
        {
            Email = user.Email;
            Id = user.Id;
            Password = user.Password;
            Groups = allGroups.ToSelectableItemList(user.UserGroups);
        }
    }

    public static class UserEditVMExtenetions
    {
        public static UserEditVM ToUserEditVM(this User user, List<Group> allGroups)
        {
            return new UserEditVM(user, allGroups);
        }
    }
}
