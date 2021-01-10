using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class FriendParticipant
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
