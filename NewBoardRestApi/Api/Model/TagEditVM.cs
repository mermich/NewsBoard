using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Api.Model
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

    public static class TagEditVMExtentions
    {
        public static TagEditVM ToTagEditVM(this Tag item)
        {
            return new TagEditVM(item);
        }
    }
}
