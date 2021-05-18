using MWEntities;
using System.Threading.Tasks;

namespace MWServices
{
    public interface ICellService
    {
        /// <summary>
        /// Check a cell in the board
        /// </summary>
        /// <param name="board">The board where the cell is placed</param>
        /// <param name="cellColumn">The position of the colunm</param>
        /// <param name="cellRow">The position of the row</param>
        /// <returns>The board with the cells</returns>
        Task<Board> CheckAsync(Board board, int cellColumn, int cellRow);

        /// <summary>
        /// Flags a cell in the board
        /// </summary>
        /// <remarks>If the cell is already flaged it set to the next status, flagged, question, clear</remarks>
        /// <param name="board">The board where the cell is placed</param>
        /// <param name="cellColumn">The position of the colunm</param>
        /// <param name="cellRow">The position of the row</param>
        /// <returns>The board with the cells</returns>
        Task<Board> FlagAsync(Board board, int cellColumn, int cellRow);


    }
}
