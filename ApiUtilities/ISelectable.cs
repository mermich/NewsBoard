namespace ApiUtilities
{
    public interface ISelectable<T>
    {
        T Value { get; set; }

        string Label { get; set; }
    }
}
