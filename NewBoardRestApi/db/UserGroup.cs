namespace NewBoardRestApi.DataModel
{
    public class UserGroup
    {
        public virtual int Id { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }


        public User User { get; set; }

        public int UserId { get; set; }


        public UserGroup()
        {
        }
    }
}