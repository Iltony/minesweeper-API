﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IApiResponse> CheckAsync(Board board, int cellColumn, int cellRow)
        {
            try
            {
                var data = await _cellService.CheckAsync(board, cellColumn, cellRow);

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
        public async Task<IApiResponse> FlagAsync(Board board, int cellColumn, int cellRow)
        {
            try
            {
                var data = await _cellService.FlagAsync(board, cellColumn, cellRow);

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
