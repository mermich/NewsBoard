using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.TagApi
{
    public static class TagEditVMExtentions
    {
        internal static TagEditVM ToTagEditVM(this Tag item)
        {
            return new TagEditVM(item);
        }
    }
}
