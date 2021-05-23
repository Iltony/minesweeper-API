using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MWEntities
{
    public class CellKeySelector
    {
        public CellKeySelector(int column, int row)
        {
            this.Column = column;
            this.Row = row;
        }
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
            // Invented some way to compare and avoid unwanted matches
            return (obj.Column * Math.Sqrt(obj.Row) - obj.Column).GetHashCode();
        }
    }
}
