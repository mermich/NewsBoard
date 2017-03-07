using System;

namespace ApiUtilities
{
    public class SelectableItemt<T>
        where T : IEquatable<T>
    {
        public T Value { get; set; }

        public bool IsSelected { get; set; }

        public string Label { get; set; }        
    }
}
