using System;

namespace ApiTools.Selectable
{
    public class SelectableItem<T>
        where T : IEquatable<T>
    {
        public T Value { get; set; }

        public bool IsSelected { get; set; }

        public string Label { get; set; }

        public SelectableItem() { }

        public SelectableItem(T value, string label, bool isSelected)
        {
            Value = value;
            Label = label;
            IsSelected = isSelected;
        }
    }
}
