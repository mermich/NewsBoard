using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.TagApi
{
    internal static class TagEditVMExtentions
    {
        internal static TagEditVM ToTagEditVM(this Tag item)
        {
            return new TagEditVM(item);
        }
    }
}
