using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.TagApi
{
    public class TagEditVM
    {
        public int Id { get; set; }

        public string Label { get; set; } = "";

        public bool Enabled { get; set; }

        public TagEditVM()
        {
        }

        public TagEditVM(Tag tag)
        {
            Id = tag.Id;
            Label = tag.Label;
            Enabled = tag.Enabled;
        }
    }
}
