using MWEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MWServices
{
    public class CellResolver : ICellResolver
    {
        public void ResolveCell(IList<Cell> boardCells, Cell cell, int level, int boardColumns, int boardRows)
        {
            // Gets the list of closer cells coords
            var closerCells = cell.GetCloserCells(boardColumns, boardRows);

            // Set status to evaluation
            cell.Status = CellStatus.Evaluation;

            IEnumerable<Cell> realCloserCells = GetCloserCellsFromBoard(boardCells, closerCells);

            // Get the mines of the cell
            cell.CloserMinesNumber = realCloserCells.Count(c => c.ItIsAMine);

            if (level <= Math.Floor((Math.Max((double)boardColumns, (double)boardRows) / 2))) {
                foreach (Cell rcc in realCloserCells.Where(c => !c.ItIsAMine))
                {
                    System.Diagnostics.Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rcc));
                    ResolveCell(boardCells, rcc, level + 1, boardColumns, boardRows);
                }
            }

            cell.Status = CellStatus.Revealed;
        }

        private static void RemoveCallerFromCloserCell(IList<Cell> closerCellsOnlyCoords, Cell caller)
        {
            closerCellsOnlyCoords.Remove(closerCellsOnlyCoords.First(c => c.Column == caller.Column && c.Row == caller.Row));
        }


        private static IEnumerable<Cell> GetCloserCellsFromBoard(IList<Cell> boardCells, IList<Cell> closerCellsOnlyCoords)
        {
            //now gets from the cells the list of cells that matches the closer coords, not mines and
            // the status is clear
            IEnumerable<Cell> result =  closerCellsOnlyCoords.Join<Cell, Cell, CellKeySelector, Cell>(
                boardCells.Where(cell => cell.Status == CellStatus.Clear || cell.ItIsAMine),
                closerCell => new CellKeySelector(closerCell.Column, closerCell.Row),
                boardCell => new CellKeySelector(boardCell.Column, boardCell.Row),
                (closerCell, boardCell) => boardCell,
                new CellEqualityComparer()
            );

            System.Diagnostics.Debug.WriteLine("closer: " + result.Count());

            return result;
        }


    }
}
