using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class Team
    {
        public Team()
        {
            TeamParticipants = new HashSet<TeamParticipant>();
        }

        public Guid Id { get; set; }
        public Guid? GameId { get; set; }

        public virtual Game Game { get; set; }
        public virtual ICollection<TeamParticipant> TeamParticipants { get; set; }
    }
}
