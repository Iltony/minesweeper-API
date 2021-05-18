using MWEntities;
using System.Collections.Generic;

namespace MWServices
{
    public interface ICellResolver
    {
        void ResolveCell(IList<Cell> boardCells, Cell cell, int boardColums, int boardRows);

    }
}
