using MWInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace MWEntities
{
    public class Cell : IListableCloserCells
    {
        public int Column { get; set; }

        public int Row { get; set; }
        
        public int Position { get; set; }

        public CellStatus Status { get; set; }

        public bool ItIsAMine { get; set; }

        public int CloserMinesNumber { get; set; }
        
        public IList<Cell> GetCloserCells(int maxColumn, int maxRow)
        {
            int currentColumn = Column;
            int currentRow = Row;

            var closerCells = new List<Cell> {
                new Cell {Column = currentColumn - 1, Row = currentRow - 1 },
                new Cell {Column = currentColumn, Row = currentRow - 1 },
                new Cell {Column = currentColumn + 1, Row = currentRow - 1},

                new Cell {Column = currentColumn-1, Row = currentRow },
                new Cell {Column = currentColumn + 1, Row = currentRow },

                new Cell {Column = currentColumn - 1, Row = currentRow + 1 },
                new Cell {Column = currentColumn, Row = currentRow + 1 },
                new Cell {Column = currentColumn + 1, Row = currentRow + 1},
            };

            // now filters these cells that are outside the borders
            return closerCells.Where(cc => (cc.Column >= 1 && cc.Column <= maxColumn) &&
                                    (cc.Row >= 1 && cc.Row <= maxRow)).ToList();

        }
    }
}
