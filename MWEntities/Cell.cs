using MWInterfaces;

namespace MWEntities
{
    public class Cell : IListableCloserCells
    {
        public int Columns { get; set; }

        public int Rows { get; set; }

        public CellStatus Status { get; set; }

        public bool ItIsAMine { get; set; }

        public int CloserMinesNumber { get; set; }

        public int GetCloserMines()
        {
            throw new System.NotImplementedException();
        }

}
}
