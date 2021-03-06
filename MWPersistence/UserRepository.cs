using MWEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWPersistence
{
    public class UserRepository : IUserRepository
    {
        private MWContext _context;

        public UserRepository(MWContext context)
        {
            _context = context;
        }

        public MWContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return await GetUserAsync(user.Username);
        }

        public async Task<bool> ExistsUserAsync(string username)
        {
            return await GetUserAsync(username) != null;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _context.Users.FindAsync(username);
        }

        public Task<IList<PersistibleBoard>> GetUserBoardsAsync(string username)
        {
            return Task.Run(() => _context.PersistibleBoards.Where(pb => pb.Username == username).ToList() as IList<PersistibleBoard>);
        }
    }
}
