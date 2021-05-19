using Microsoft.AspNetCore.Mvc;
using minesweeper_API.Controllers;
using System.Threading.Tasks;

namespace minesweeper_API
{
    public interface ICellController
    {
        /// <summary>
        /// Check a cell in the board
        /// </summary>
        /// <param name="cellRequest">The request for the method</param>
        /// <returns>The board with the cells</returns>
        Task<IApiResponse> CheckAsync([FromBody] CellRequest cellRequest);

        /// <summary>
        /// Flags a cell in the board
        /// </summary>
        /// <remarks>If the cell is already flaged it set to the next status, flagged, question, clear</remarks
        /// <param name="cellRequest">The request for the method</param>
        /// <returns>The board with the cells</returns>
        Task<IApiResponse> FlagAsync([FromBody] CellRequest cellRequest);
    }
}