using System;
using GomokuAdmin.Data.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GameResult>("game_gameresult_enum")
                   .MapEnum<TeamSide>("team_side_enum")
                   .MapEnum<GameType>("game_gameendingtype_enum");
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GameResult>("game_gameresult_enum")
                   .MapEnum<TeamSide>("team_side_enum")
                   .MapEnum<GameType>("game_gameendingtype_enum");
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ChatChannel> ChatChannels { get; set; }
        public virtual DbSet<ChatParticipant> ChatParticipants { get; set; }
        public virtual DbSet<ChatRecord> ChatRecords { get; set; }
        public virtual DbSet<FriendParticipant> FriendParticipants { get; set; }
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<MoveRecord> MoveRecords { get; set; }
        public virtual DbSet<RankRecord> RankRecords { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamParticipant> TeamParticipants { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ec2-35-169-184-61.compute-1.amazonaws.com;Database=d2qd61lb9o6l7o;Username=tkedrqcviqvflz;Password=2fab4fdba4eee4dac0b87d04efc90430a4d89f2ef23d331a4d0fe0c657edffbe;sslmode=Require;Trust Server Certificate=true;");

            }
           
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.HasPostgresEnum(null, "game_gameendingtype_enum", new[] { "normal", "timeout", "surrender", "quit" })
                .HasPostgresEnum(null, "game_gameresult_enum", new[] { "0", "1", "2" })
                .HasPostgresEnum(null, "move_record_value_enum", new[] { "0", "1" })
                .HasPostgresEnum(null, "team_side_enum", new[] { "0", "1" })
                .HasPostgresExtension("adminpack")
                .HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "English_United Kingdom.1252");
           

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.HasIndex(e => e.Username, "UQ_5e568e001f9d1b91f67815c580f")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ_de87485f6489f5d0995f5841952")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BannedAt)
                    .HasColumnName("banned_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("firstName")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("lastName")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.PhotoUrl)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("photoURL")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.ResetPasswordExpires)
                    .HasColumnName("resetPasswordExpires")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ResetPasswordToken)
                    .HasColumnType("character varying")
                    .HasColumnName("resetPasswordToken");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("username");
            });

            modelBuilder.Entity<ChatChannel>(entity =>
            {
                entity.ToTable("chat_channel");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<ChatParticipant>(entity =>
            {
                entity.HasKey(e => new { e.ChatChannelId, e.UserId })
                    .HasName("PK_079160b998cf91e365e4ae2249d");

                entity.ToTable("chat_participant");

                entity.HasIndex(e => e.ChatChannelId, "IDX_0575881411390ce05395710648");

                entity.HasIndex(e => e.UserId, "IDX_b222da2539ff0a06a8f5493f86");

                entity.Property(e => e.ChatChannelId).HasColumnName("chatChannelId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.ChatChannel)
                    .WithMany(p => p.ChatParticipants)
                    .HasForeignKey(d => d.ChatChannelId)
                    .HasConstraintName("FK_0575881411390ce053957106485");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatParticipants)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_b222da2539ff0a06a8f5493f868");
            });

            modelBuilder.Entity<ChatRecord>(entity =>
            {
                entity.ToTable("chat_record");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ChannelId).HasColumnName("channelId");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.ChatRecords)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_58a8de1b10b5d5d214cf8eeb0ac");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatRecords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_b36fed798182dfaa34da353f61f");
            });

            modelBuilder.Entity<FriendParticipant>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.User1Id, e.User2Id })
                    .HasName("PK_f566c39602e104b9cd07c184fae");

                entity.ToTable("friend_participant");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.User1Id).HasColumnName("user1Id");

                entity.Property(e => e.User2Id).HasColumnName("user2Id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.User1)
                    .WithMany(p => p.FriendParticipantUser1s)
                    .HasForeignKey(d => d.User1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bffc5d4cb2d6d116cdb3e6ad7e8");

                entity.HasOne(d => d.User2)
                    .WithMany(p => p.FriendParticipantUser2s)
                    .HasForeignKey(d => d.User2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_80df11a783aafae557e64ce325e");
            });

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.SenderId, e.ReceiverId })
                    .HasName("PK_82cb9e67d294d7cb71c83ffe489");

                entity.ToTable("friend_request");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.Property(e => e.ReceiverId).HasColumnName("receiverId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.FriendRequestReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_470e723fdad9d6f4981ab2481eb");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.FriendRequestSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_9509b72f50f495668bae3c0171c");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BoardSize)
                    .HasColumnName("boardSize")
                    .HasDefaultValueSql("20");

                entity.Property(e => e.ChatId).HasColumnName("chatId");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.StartAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("start_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.WinningLine)
                    .HasColumnType("character varying")
                    .HasColumnName("winningLine");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.ChatId)
                    .HasConstraintName("FK_ed6b8c488ab1b1d81c897e8b877");
            })
                ;

            modelBuilder.Entity<MoveRecord>(entity =>
            {
                entity.ToTable("move_record");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.MoveRecords)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_922fcb19673f3f3550d40ac6f8e");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MoveRecords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_c2ddf0c4bb4d55189a599910ea2");
            });

            modelBuilder.Entity<RankRecord>(entity =>
            {
                entity.ToTable("rank_record");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.NewRank).HasColumnName("newRank");

                entity.Property(e => e.OldRank).HasColumnName("oldRank");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.RankRecords)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_fc27254878fffdcd585f41ba61a");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RankRecords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_38a7218b0693db413e31dd2d575");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_2dad5b2c6156806e8fd59bf37b5");
            });

            modelBuilder.Entity<TeamParticipant>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TeamId })
                    .HasName("PK_81539c1ce149295a2faa0d515d6");

                entity.ToTable("team_participant");

                entity.HasIndex(e => e.TeamId, "IDX_4b3b36e735afa79c335f2fea69");

                entity.HasIndex(e => e.UserId, "IDX_ec462bbd7c2dd4f79aebfbf2d8");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.TeamId).HasColumnName("teamId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamParticipants)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_4b3b36e735afa79c335f2fea693");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamParticipants)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ec462bbd7c2dd4f79aebfbf2d81");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Username, "UQ_78a916df40e02a9deb1c4b75edb")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ_e12875dfb3b1d92d7d7c5377e22")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ActivateCode)
                    .HasColumnType("character varying")
                    .HasColumnName("activateCode");

                entity.Property(e => e.ActivatedAt)
                    .HasColumnName("activated_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.BannedAt)
                    .HasColumnName("banned_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("firstName")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("lastName")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.NumberOfMatches).HasColumnName("numberOfMatches");

                entity.Property(e => e.NumberOfWonMatches).HasColumnName("numberOfWonMatches");

                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.PhotoUrl)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("photoURL")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasDefaultValueSql("1000");

                entity.Property(e => e.ResetPasswordExpires)
                    .HasColumnName("resetPasswordExpires")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ResetPasswordToken)
                    .HasColumnType("character varying")
                    .HasColumnName("resetPasswordToken");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
