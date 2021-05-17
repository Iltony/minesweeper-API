using MWEntities;
using System;
using System.Threading.Tasks;

namespace minesweeper_API
{
    public interface ICellController
    {
        /// <summary>
        /// Check a cell in the board
        /// </summary>
        /// <param name="board">The board where the cell is placed</param>
        /// <param name="cellColumn">The position of the colunm</param>
        /// <param name="cellRow">The position of the row</param>
        /// <returns>The board with the cells</returns>
        Task<IApiResponse> CheckAsync(Board board, int cellColumn, int cellRow);

        /// <summary>
        /// Flags a cell in the board
        /// </summary>
        /// <remarks>If the cell is already flaged it set to the next status, flagged, question, clear</remarks
        /// <param name="board">The board where the cell is placed</param>
        /// <param name="cellColumn">The position of the colunm</param>
        /// <param name="cellRow">The position of the row</param>
        /// <returns>The board with the cells</returns>
        Task<IApiResponse> FlagAsync(Board board, int cellColumn, int cellRow);
    }
}