using MWEntities;

namespace minesweeper_API.Controllers
{
    public class InitializeRequest
    {
        public CellCoordinates InitialClickCell { get; set; }

        public string Username { get; set; }

        public int Columns { get; set; } = 10;

        public int Rows { get; set; } = 10;

        public int Mines { get; set; } = 10;

    }
}
