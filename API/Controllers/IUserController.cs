using System;
using System.Threading.Tasks;

namespace minesweeper_API.Controllers
{
    public interface IUserController
    {
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="username">The username to be registered. It cannot exist in the database</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="birthdate">Birth Date of the user</param>6
        /// <returns>The Api result with the registered user with status success</returns>
        /// <returns>Status error in case of existing username or username length <= 1 character or age less than 10 years</returns>
        /// <returns>The Api result with the registered user with status success</returns>
        Task<IApiResponse> RegisterAsync(string username, string firstName, string lastName, DateTime birthdate);

        /// <summary>
        /// For a user it returns the saved board
        /// </summary>
        /// <param name="username">The username to retrieve the boards</param>
        /// <returns>Returns the stored board for the user</returns>
        Task<IApiResponse> GetUserBoardsAsync(string username);
    }
}