using MWEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MWServices
{
    public interface IUserService
    {
        /// <summary>
        /// Validates if the username exists in the database
        /// </summary>
        /// <param name="username">the username to lookup</param>
        /// <returns>Return true if the user exists, otherwise false</returns>
        Task<bool> ExistsUserAsync(string username);

        /// <summary>
        /// Takes the user information and create the user in the database
        /// </summary>
        /// <param name="username">The nick for the user</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="birthdate">The birthdate of the user</param>
        /// <returns>It returns the created user in the database</returns>
        Task<User> RegisterUserAsync(string username, string firstName, string lastName, DateTime birthdate);


        /// <summary>
        /// Retrieve the list of saved boards for the user
        /// </summary>
        /// <param name="username">The username for lookup the user</param>
        /// <returns>It returns the list of boards for the user</returns>
        Task<IList<Board>> GetUserBoardsAsync(string username);
    }
}
