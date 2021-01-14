using GomokuAdmin.Data.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid GameId { get; set; }

        [Column("side")]
        public TeamSide Side { set; get; }

        public virtual Game Game { get; set; }
        public virtual ICollection<TeamParticipant> TeamParticipants { get; set; }
    }
}
