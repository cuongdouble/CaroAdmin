using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class ChatRecord
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ChannelId { get; set; }

        public virtual ChatChannel Channel { get; set; }
        public virtual User User { get; set; }
    }
}
