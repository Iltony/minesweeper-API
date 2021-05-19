using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MWEntities
{
    public class CellKeySelector
    {
        public int Column { get; set; }

        public int Row{ get; set; }
    }

    public class CellEqualityComparer: IEqualityComparer<CellKeySelector> 
    {
        public bool Equals(CellKeySelector x, CellKeySelector y)
        {
            return x.Column == y.Column && x.Row == y.Row;
        }

        public int GetHashCode([DisallowNull] CellKeySelector obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }
}
