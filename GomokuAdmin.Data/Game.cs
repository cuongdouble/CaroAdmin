using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class Game
    {
        public Game()
        {
            MoveRecords = new HashSet<MoveRecord>();
            RankRecords = new HashSet<RankRecord>();
            Teams = new HashSet<Team>();
        }

        public Guid Id { get; set; }
        public int BoardSize { get; set; }
        public DateTime StartAt { get; set; }
        public double? Duration { get; set; }
        public Guid? ChatId { get; set; }

        public virtual ChatChannel Chat { get; set; }
        public virtual ICollection<MoveRecord> MoveRecords { get; set; }
        public virtual ICollection<RankRecord> RankRecords { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
