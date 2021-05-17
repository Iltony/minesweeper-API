using MWEntities;
using System;
using System.Threading.Tasks;

namespace MWServices
{
    public interface IBoardService
    {
        ///// <summary>
        ///// For a user it returns the saved board
        ///// </summary>
        ///// <param name="username">The username to retrieve the boards</param>
        ///// <returns>Returns the stored board for the user</returns>
        Task<Board> SaveBoardAsync(Board board);

        /// <summary>
        /// Resume an existing board
        /// </summary>
        /// <param name="boardId">The board Id to resume</param>
        /// <param name="currentUserName">The username to validate with the board</param>
        /// <remarks>If the user does not match an error is raised</remarks>
        /// <returns>The saved board to resume</returns>
        Task<Board> ResumeAsync(Guid boardId, string currentUserName);

        /// <summary>
        /// Initialize the board with the specified properties
        /// </summary>
        /// <param name="username">The current user name</param>
        /// <param name="columns">The number of columns, by default 10</param>
        /// <param name="rows">The number of rows, by default 10</param>
        /// <param name="mines">The number of mines></param>
        /// <returns>The initialized board</returns>
        Task<Board> InitializeAsync(string username, int columns, int rows, int mines);

    }
}
