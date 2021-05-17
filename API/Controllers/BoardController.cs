using Microsoft.AspNetCore.Mvc;
using MWEntities;
using MWServices;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace minesweeper_API.Controllers
{
    //[Authorize]
    [Route("api/board")]
    [ApiController]
    public class BoardController : ControllerBase, IBoardController
    {
        private IBoardService _boardService;
        private IServicesResourceManager _serviceResourceManager;

        public BoardController(IBoardService boardService, IServicesResourceManager serviceResourceManager) : base()
        {
            _boardService = boardService;
            _serviceResourceManager = serviceResourceManager;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IApiResponse> SaveBoardAsync(Board board)
        {
            var message = _serviceResourceManager.ResourceManager.GetString("BoardSaved");

            try
            {
                var data = await _boardService.SaveBoardAsync(board);

                return new SuccessResponse<Board>
                {
                    Data = data,
                    Message = message
                };

            }
            catch (Exception ex)
            {
                return (ex is InvalidBoardException) ? 
                    new ErrorResponse { Message = ex.Message } : 
                    new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
            }
        }

        [HttpPost]
        [Route("resume")]
        public async Task<IApiResponse> ResumeAsync(Guid boardId, string currentUserName)
        {
            var message = _serviceResourceManager.ResourceManager.GetString("BoardResumed");

            try
            {
                var data = await _boardService.ResumeAsync(boardId, currentUserName);

                return new SuccessResponse<Board>
                {
                    Data = data,
                    Message = message
                };

            }
            catch (Exception ex)
            {
                return (ex is InvalidBoardException || ex is InvalidBoardForCurrentUserException) ?
                    new ErrorResponse { Message = ex.Message } :
                    new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
            }
        }

        [HttpPost]
        [Route("initialize")]
        public async Task<IApiResponse> InitializeAsync(string username, int columns = 10, int rows = 10, int mines = 10)
        {
            var message = _serviceResourceManager.ResourceManager.GetString("BoardInitialized");

            try
            {
                var data = await _boardService.InitializeAsync(username, columns, rows, mines);

                return new SuccessResponse<Board>
                {
                    Data = data,
                    Message = message
                };

            }
            catch (Exception ex)
            {
                return (ex is InvalidUsernameException) ?
                    new ErrorResponse { Message = ex.Message } :
                    new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
            }
        }
    }
}
