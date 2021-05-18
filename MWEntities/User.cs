using System;

namespace MWEntities
{
    public class User
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public Int64 MillisecondsRecord { get; set; }
    }
}
