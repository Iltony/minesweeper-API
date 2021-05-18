using MWEntities;
using System;
using System.Threading.Tasks;

namespace MWPersistence
{
    public class BoardRepository : IBoardRepository
    {
        private MWContext _context;

        public BoardRepository(MWContext context)
        {
            _context = context;
        }

        public MWContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public async Task<PersistibleBoard> SaveBoardAsync(PersistibleBoard board)
        {
            if (GetBoardAsync(board.Username, board.BoardId) == null)
                _context.PersistibleBoards.Add(board);
            else
                _context.PersistibleBoards.Update(board);

            await _context.SaveChangesAsync();

            return await GetBoardAsync(board.Username, board.BoardId);
        }

        public async Task<PersistibleBoard> GetBoardAsync(string username, Guid boardId)
        {
            return await _context.PersistibleBoards.FindAsync(username, boardId);
        }

        public async Task DeleteBoardAsync(string username, Guid boardId)
        {
            var entity = await GetBoardAsync(username, boardId);

            if (entity != null)
            {
                _context.PersistibleBoards.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
