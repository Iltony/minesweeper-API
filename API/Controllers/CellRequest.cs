using MWEntities;

namespace minesweeper_API.Controllers
{
    public class CellRequest
    {
        public Board Board { get; set; }

        public CellCoordinates Cell{ get; set; }
    }
}
