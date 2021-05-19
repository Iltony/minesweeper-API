using Microsoft.AspNetCore.Mvc;
using minesweeper_API.Controllers;
using MWEntities;
using System.Threading.Tasks;

namespace minesweeper_API
{
    public interface IBoardController
    {
        ///// <summary>
        ///// For a user it returns the saved board
        ///// </summary>
        ///// <param name="username">The username to retrieve the boards</param>
        ///// <returns>Returns the stored board for the user</returns>
        Task<IApiResponse> SaveBoardAsync([FromBody] Board board);

        /// <summary>
        /// Resume an existing board
        /// </summary>
        /// <param name="boardId">The board Id to resume</param>
        /// <param name="currentUserName">The username to validate with the board</param>
        /// <remarks>If the user does not match an error is raised</remarks>
        /// <returns>The saved board to resume</returns>
        Task<IApiResponse> ResumeAsync([FromBody] ResumeRequest resumeRequest);

        /// <summary>
        /// Initialize the board with the specified properties
        /// </summary>
        /// <param name="initializeRequest">The initialize object request properties></param>
        /// <returns>The initialized board</returns>
        Task<IApiResponse> InitializeAsync([FromBody] InitializeRequest initializeRequest);

    }
}

