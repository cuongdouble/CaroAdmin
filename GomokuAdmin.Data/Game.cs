using GomokuAdmin.Data.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid? ChatId { get; set; }

        [Column("gameResult")]
        public GameResult? GameResult { get; set; }
        public double? Duration { get; set; }
        public string WinningLine { get; set; }
        [Column("gameEndingType")]
        public GameType GameType { get; set; }
        public virtual ChatChannel Chat { get; set; }
        public virtual ICollection<MoveRecord> MoveRecords { get; set; }
        public virtual ICollection<RankRecord> RankRecords { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
