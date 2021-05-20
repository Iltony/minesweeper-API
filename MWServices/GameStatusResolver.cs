using MWEntities;
using System.Linq;
using System.Threading.Tasks;

namespace MWServices
{
    public class GameStatusResolver : IGameStatusResolver
    {
        public Task<Board> EvaluateGameStatus(Board board)
        {
            if (board.GameStatus == GameStatus.Active)
            {
                int flagged = board.Cells.Count(c => c.Status == CellStatus.Flagged && c.ItIsAMine == true);
                int flaggedIncorrectly = board.Cells.Count(c => c.Status == CellStatus.Flagged && c.ItIsAMine == false);
                int mines = board.Cells.Count(c => c.ItIsAMine);

                // Won game if all the flags are set
                if (flaggedIncorrectly == 0 && flagged == mines) {

                    // now we evaluate that all the mines flagged are the mines
                    board.GameStatus = GameStatus.Resolved;
                }
            }

            return Task.FromResult(board);
        }
    }
}
