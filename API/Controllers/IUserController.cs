using System;
using System.Threading.Tasks;

namespace minesweeper_API.Controllers
{
    public interface IUserController
    {
        Task<IApiResponse> RegisterAsync(string username, string firstName, string lastName, DateTime birthdate);
    }
}