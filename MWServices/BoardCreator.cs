using MWEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MWServices
{
    public class BoardCreator : IBoardCreator
    {

        public Board GenerateBoard(Cell initialClickCell, User user, int columns, int rows, int mines)
        {
            IList<Cell> cells = new List<Cell>();
            IList<Cell> minesCells = new List<Cell>();

            GetDefaultCells(columns, rows, cells);
            GenerateMinesForTheBoard(initialClickCell, mines, cells, minesCells);

            var newBoard = new Board
            {
                Columns = columns,
                Rows = rows,
                GameStatus = GameStatus.Active,
                Milliseconds = 0,
                Mines = minesCells,
                Cells = cells,
                Id = Guid.NewGuid(),
                Name = null,
                Owner = user,
            };

            return newBoard;
        }

        private void GenerateMinesForTheBoard(Cell initialClickCell, int mines, IList<Cell> cells, IList<Cell> minesCells)
        {
            var rdm = new Random();

            if (mines >= cells.Count) {
                var updatedCells = cells
                    .Select(c => { c.ItIsAMine = true; return c; }).ToList();
                minesCells = cells;
            }
            else
            {
                while (minesCells.Count < mines)
                {
                    var newMinePosition = rdm.Next(1, cells.Count);

                    if (cells.Any(c => c.Position == newMinePosition && !c.ItIsAMine))
                    {
                        // takes the element with the position and set the it's mine flag
                        var cell = cells.Where<Cell>(c => c.Position == newMinePosition && !c.ItIsAMine).First();

                        if (!(cell.Column == initialClickCell.Column && cell.Row == initialClickCell.Row))
                        {
                            cell.ItIsAMine = true;

                            minesCells.Add(cell);
                        }
                    }
                }
            }
        }

        private static void GetDefaultCells(int columns, int rows, IList<Cell> cells)
        {
            // Fills the cells
            var pos = 1;

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= columns; j++)
                {
                    var newCell = new Cell
                    {
                        Row = i,
                        Column = j,
                        Position = pos,
                        CloserMinesNumber = 0,
                        ItIsAMine = false,
                        Status = CellStatus.Clear,
                    };

                    cells.Add(newCell);
                    pos++;
                }
            }
        }
    }
}
