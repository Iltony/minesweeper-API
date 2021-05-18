using MWEntities;
using System.Threading.Tasks;

namespace MWServices
{
    public interface IGameProcessor
    {
        Board GenerateBoard(Cell initialClickCell,  User user, int columns, int rows, int mines);

        Task<Board> CheckAsync(Board board, int column, int rows);

        Task<Board> FlagAsync(Board board, int column, int rows);
    }
}
