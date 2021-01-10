using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class MoveRecord
    {
        public Guid Id { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId { get; set; }
        public Guid? GameId { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
