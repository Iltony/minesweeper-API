using MWEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MWPersistence
{
    public interface IUserRepository
    {
        MWContext Context { get; set; }

        Task<User> GetUserAsync(string username);

        Task<User> RegisterUserAsync(User user);

        Task<bool> ExistsUserAsync(string username);

        Task<IList<PersistibleBoard>> GetUserBoardsAsync(string username);

    }
}