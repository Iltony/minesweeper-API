using System;
using System.Collections.Generic;

namespace MWEntities
{
    public class Board
    {
        public Guid Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public long Milliseconds { get; set; }
        public IList<Cell> Cells { get; set; }
        public IList<Cell> Mines { get; set; }
        public GameStatus GameStatus { get; set; }

    }
}
