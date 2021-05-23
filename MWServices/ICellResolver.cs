using MWEntities;
using System.Collections.Generic;

namespace MWServices
{
    public interface ICellResolver
    {
        /// <summary>
        /// resolve the values after a user interactions
        /// </summary>
        /// <param name="boardCells">All the cells of the board</param>
        /// <param name="cell">the currently evaluated cell</param>
        /// <param name="level">the level of evaluation,  to make it interactive will be show only some cells</param>
        /// <param name="boardColumns">number of columns of the board</param>
        /// <param name="boardRows">number of rows of the board</param>
        void ResolveCell(IList<Cell> boardCells, Cell cell, int level, int boardColumns, int boardRows);

    }
}
