using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class ChatParticipant
    {
        public Guid ChatChannelId { get; set; }
        public Guid UserId { get; set; }

        public virtual ChatChannel ChatChannel { get; set; }
        public virtual User User { get; set; }
    }
}
