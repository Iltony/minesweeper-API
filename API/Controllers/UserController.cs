using Microsoft.AspNetCore.Mvc;
using MWEntities;
using MWServices;
using System;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace minesweeper_API.Controllers
{
    //[Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase, IUserController
    {
        private IUserService _userService;
        private IServicesResourceManager _serviceResourceManager;

        public UserController(IUserService userService, IServicesResourceManager serviceResourceManager) : base()
        {
            _userService = userService;
            _serviceResourceManager = serviceResourceManager;
        }

        [HttpPut]
        [Route("register")]
        public async Task<IApiResponse> RegisterAsync(string username, string firstName, string lastName, DateTime birthdate)
        {
            var cult = Thread.CurrentThread.CurrentUICulture;
            var message = _serviceResourceManager.ResourceManager.GetString("UserRegistered");

            try
            {
                var data = await _userService.RegisterUserAsync(username, firstName, lastName, birthdate);

                return new SuccessResponse<User>
                {
                    Data = data,
                    Message = message
                };

            }
            catch (Exception ex)
            {
                if (ex is DuplicatedUsernameException || ex is InvalidUsernameException)
                {
                    return new ErrorResponse { Message = ex.Message };
                }
                else
                {
                    return new ErrorResponse { Message = _serviceResourceManager.ResourceManager.GetString("DefaultErrorMessage") };
                }
            }
        }
    }
}
