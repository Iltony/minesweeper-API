using MWInterfaces;
using System.Collections.Generic;

namespace MWEntities
{
    public class Cell : IListableCloserCells
    {
        public int Columns { get; set; }

        public int Rows { get; set; }

        public CellStatus Status { get; set; }

        public bool ItIsAMine { get; set; }

        public int CloserMinesNumber { get; set; }

        public Cell GetCloserMines()
        {
            throw new System.NotImplementedException();
        }

        IList<Cell> IListableCloserCells.GetCloserMines()
        {
            throw new System.NotImplementedException();
        }
    }
}
