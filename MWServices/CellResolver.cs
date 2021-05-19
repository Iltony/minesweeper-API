using MWEntities;
using System.Collections.Generic;
using System.Linq;

namespace MWServices
{
    public class CellResolver : ICellResolver
    {
        public void ResolveCell(IList<Cell> boardCells, Cell cell, int boardColums, int boardRows)
        {
            // Gets the list of closer cells coords
            var closerCells = cell.GetCloserCells(boardColums, boardRows);

            //now gets from the cells the list of cells that matches
            var realCloserCells = boardCells.Join<Cell, Cell, CellKeySelector, Cell> (
                closerCells,
                boardCell => new CellKeySelector { Column = boardCell.Column, Row = boardCell.Row }, 
                closerCell => new CellKeySelector { Column= closerCell.Column, Row = closerCell.Row },
                (boardCell, closerCell) => boardCell, 
                new CellEqualityComparer()
            );

            int minesCounter = 0;

            foreach (Cell rcc in realCloserCells)
            {
                if (rcc.ItIsAMine)
                {
                    minesCounter++;
                }
                else if (rcc.Status == CellStatus.Clear)
                {
                    ResolveCell(boardCells, rcc, boardColums, boardRows);
                }
            }

            boardCells.First(c => c.Column == cell.Column && c.Row == cell.Row).Status = CellStatus.Revealed;
            boardCells.First(c => c.Column == cell.Column && c.Row == cell.Row).CloserMinesNumber = minesCounter;
        }
    }
}
