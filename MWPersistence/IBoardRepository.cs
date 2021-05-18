using MWEntities;
using System;
using System.Threading.Tasks;

namespace MWPersistence
{
    public interface IBoardRepository
    {
        MWContext Context { get; set; }

        Task<PersistibleBoard> SaveBoardAsync(PersistibleBoard board);

        Task DeleteBoardAsync(string username, Guid boardId);

        Task<PersistibleBoard> GetBoardAsync(string username, Guid boardId);
    }
}