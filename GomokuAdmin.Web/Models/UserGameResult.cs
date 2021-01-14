using GomokuAdmin.Data;
using GomokuAdmin.Data.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GomokuAdmin.Web.Models
{
    public class UserGameResult
    {
        public Guid Id { get; set; }
        public int BoardSize { get; set; }
        public DateTime StartAt { get; set; }
        public Guid? ChatId { get; set; }
        public string Result { get; set; }
        public double? Duration { get; set; }
        public string WinningLine { get; set; }
        public GameType GameType { get; set; }
    }
}
