using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.PermissionApi
{
    public class PermissionVM
    {
        public int Id { get; set; }


        public string Label { get; set; } = "";


        public string Key { get; set; } = "";


        public PermissionVM()
        {
        }


        public PermissionVM(Permission perm)
        {
            Id = perm.Id;
            Label = perm.Label;
            Key = perm.Key;
        }
    }
}
