using MWEntities;
using System.Threading.Tasks;

namespace MWServices
{
    public interface IGameStatusResolver
    {
        Task<Board> EvaluateGameStatus(Board board);
    }
}
