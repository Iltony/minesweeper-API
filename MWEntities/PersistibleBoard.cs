using System;

namespace MWEntities
{
    public class PersistibleBoard
    {
        public string Username { get; set; }

        public Guid BoardId { get; set; }

        public string BoardName { get; set; }

        public string BoardDefinition { get; set; }

    }
}
