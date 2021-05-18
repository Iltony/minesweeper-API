using MWEntities;
using System.Collections.Generic;

namespace MWInterfaces
{
    public interface IListableCloserCells
    {
        int CloserMinesNumber { get; set; }

        IList<Cell> GetCloserCells(int maxColumns, int maxRows);
    }
}
