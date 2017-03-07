using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUtilities
{
    public interface ISelectable<T>
    {
        T Value { get; set; }

        string Label { get; set; }
    }
}
