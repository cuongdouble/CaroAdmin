using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class User
    {
        public User()
        {
            ChatParticipants = new HashSet<ChatParticipant>();
            ChatRecords = new HashSet<ChatRecord>();
            FriendParticipantUser1s = new HashSet<FriendParticipant>();
            FriendParticipantUser2s = new HashSet<FriendParticipant>();
            FriendRequestReceivers = new HashSet<FriendRequest>();
            FriendRequestSenders = new HashSet<FriendRequest>();
            MoveRecords = new HashSet<MoveRecord>();
            RankRecords = new HashSet<RankRecord>();
            TeamParticipants = new HashSet<TeamParticipant>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int NumberOfMatches { get; set; }
        public int NumberOfWonMatches { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public bool BanChat { get; set; }

        public virtual ICollection<ChatParticipant> ChatParticipants { get; set; }
        public virtual ICollection<ChatRecord> ChatRecords { get; set; }
        public virtual ICollection<FriendParticipant> FriendParticipantUser1s { get; set; }
        public virtual ICollection<FriendParticipant> FriendParticipantUser2s { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestSenders { get; set; }
        public virtual ICollection<MoveRecord> MoveRecords { get; set; }
        public virtual ICollection<RankRecord> RankRecords { get; set; }
        public virtual ICollection<TeamParticipant> TeamParticipants { get; set; }
    }
}
