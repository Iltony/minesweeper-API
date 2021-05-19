using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MWEntities;
using MWServices;
using System;
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
        private IMapper _mapper;

        public BoardController(IBoardService boardService, IServicesResourceManager serviceResourceManager, IMapper mapper) : base()
        {
            _boardService = boardService;
            _serviceResourceManager = serviceResourceManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IApiResponse> SaveBoardAsync([FromBody] Board board)
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
        public async Task<IApiResponse> ResumeAsync([FromBody] ResumeRequest resumeRequest)
        {
            var message = _serviceResourceManager.ResourceManager.GetString("BoardResumed");

            try
            {
                var data = await _boardService.ResumeAsync(resumeRequest.BoardId, resumeRequest.CurrentUserName);

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
        public async Task<IApiResponse> InitializeAsync([FromBody] InitializeRequest initializeRequest)
        {
            var message = _serviceResourceManager.ResourceManager.GetString("BoardInitialized");

            try
            { 
                var cell = _mapper.Map<Cell>(initializeRequest.InitialClickCell);
                var data = await _boardService.InitializeAsync(
                                                cell, 
                                                initializeRequest.Username, 
                                                initializeRequest.Columns, 
                                                initializeRequest.Rows, 
                                                initializeRequest.Mines);

                return new SuccessResponse<Board>
                {
                    Data = data,
                    Message = message
                };

            }
            catch (Exception ex)
            {
                return (ex is InvalidUsernameException || ex is InvalidCellException) ?
                    new ErrorResponse { Message = ex.Message } :
                    new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
            }
        }
    }
}
