using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class ChatChannel
    {
        public ChatChannel()
        {
            ChatParticipants = new HashSet<ChatParticipant>();
            ChatRecords = new HashSet<ChatRecord>();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Game Game { get; set; }
        public virtual ICollection<ChatParticipant> ChatParticipants { get; set; }
        public virtual ICollection<ChatRecord> ChatRecords { get; set; }
    }
}
