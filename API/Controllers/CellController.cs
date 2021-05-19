using Microsoft.AspNetCore.Mvc;
using MWEntities;
using MWServices;
using System;
using System.Threading.Tasks;

namespace minesweeper_API.Controllers
{
    //[Authorize]
    [Route("api/cell")]
    [ApiController]
    public class CellController : ControllerBase, ICellController
    {
        private ICellService _cellService;
        private IServicesResourceManager _serviceResourceManager;

        public CellController(ICellService cellService, IServicesResourceManager serviceResourceManager) : base()
        {
            _cellService = cellService;
            _serviceResourceManager = serviceResourceManager;
        }

        [HttpPost]
        [Route("check")]
        public async Task<IApiResponse> CheckAsync([FromBody] CellRequest cellRequest)
        {
            try
            {
                var data = await _cellService.CheckAsync(cellRequest.Board, cellRequest.Cell.Column, cellRequest.Cell.Row);

                return new SuccessResponse<Board>
                {
                    Data = data,
                };

            }
            catch (Exception ex)
            {
                return (ex is GameNotStartedException || ex is InvalidCellException) ?
                    new ErrorResponse { Message = ex.Message } :
                    new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
            }
        }

        [HttpPost]
        [Route("flag")]
        public async Task<IApiResponse> FlagAsync([FromBody] CellRequest cellRequest)
        {
            try
            {
                var data = await _cellService.FlagAsync(cellRequest.Board, cellRequest.Cell.Column, cellRequest.Cell.Row);

                return new SuccessResponse<Board>
                {
                    Data = data,
                };

            }
            catch (Exception ex)
            {
                return (ex is GameNotStartedException || ex is InvalidCellException) ?
                    new ErrorResponse { Message = ex.Message } :
                    new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
            }
        }
    }
}
