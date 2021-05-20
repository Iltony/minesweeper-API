using System;

namespace minesweeper_API.Controllers
{
    public class RegisterRequest
    {
        public string Username { get; set; }
     
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime Birthdate { get; set; }
    }
}
