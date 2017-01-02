namespace NewBoardRestApi.DataModel
{
    public class GroupPermission
    {
        public virtual int Id { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }


        public Permission Permission { get; set; }

        public int PermissionId { get; set; }


        public GroupPermission()
        {
        }
    }
}