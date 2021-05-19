using System;

namespace minesweeper_API.Controllers
{
    public class ResumeRequest
    {
        public Guid BoardId { get; set; }

        public string CurrentUserName { get; set; }
    }
}
