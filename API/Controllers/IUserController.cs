using System.Threading.Tasks;

namespace minesweeper_API.Controllers
{
    public interface IUserController
    {
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="registerRequest">The request with the user to be registered</param>
        /// <returns>The Api result with the registered user with status success</returns>
        /// <returns>Status error in case of existing username or username length <= 1 character or age less than 10 years</returns>
        /// <returns>The Api result with the registered user with status success</returns>
        Task<IApiResponse> RegisterAsync(RegisterRequest registerRequest);

        /// <summary>
        /// For a user it returns the saved board
        /// </summary>
        /// <param name="username">The username to retrieve the boards</param>
        /// <returns>Returns the stored board for the user</returns>
        Task<IApiResponse> GetUserBoardsAsync(string username);

        /// <summary>
        /// Get the user with the corresponding username
        /// </summary>
        /// <param name="username">The username to retrieve</param>
        /// <returns>Returns the stored user or null if it does not exists</returns>
        Task<IApiResponse> GetUserAsync(string username);
    }
}