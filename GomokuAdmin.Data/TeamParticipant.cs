using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class TeamParticipant
    {
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
