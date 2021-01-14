using GomokuAdmin.Data;
using GomokuAdmin.Data.Constraints;
using GomokuAdmin.Web.Infrastructure;
using GomokuAdmin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GomokuAdmin.Web.Services
{
    public class GameService : ServiceBase
    {
        private postgresContext _dbContext;
        public GameService()
        {
            _dbContext = new postgresContext();
        }

        public virtual Result<List<Game>> GetAll()
        {
            return Ok(_dbContext.Games.ToList());
        }

        public virtual Result<List<Game>> Search(Guid? userId)
        {
            if(userId==null || string.IsNullOrEmpty(userId?.ToString()))
                return Ok(_dbContext.Games.OrderByDescending(x=>x.StartAt).ToList());
            //var user = _dbContext.Users.Where(x => x.Id.Equals(userId)).FirstOrDefault();
            var game = from tp in _dbContext.TeamParticipants
                       where tp.UserId.Equals(userId)
                       join t in _dbContext.Teams on tp.TeamId equals t.Id
                       join g in _dbContext.Games on t.GameId equals g.Id
                       orderby g.StartAt descending
                       select g;
            return Ok(game.ToList());
        }

        public virtual Result<List<ChatContent>> GetChat(Guid gameId)
        {
            var game = _dbContext.Games.Where(x => x.Id.Equals(gameId)).FirstOrDefault();
           
            if (game == null)
                return Error<List<ChatContent>>("Game not found");
            var duration = game.Duration ?? 0;
            var chat = from cc in _dbContext.ChatChannels
                       where cc.Id.Equals(game.ChatId)
                       join cr in _dbContext.ChatRecords on cc.Id equals cr.ChannelId
                       where (cr.CreatedAt >= game.StartAt && cr.CreatedAt<=game.StartAt.AddSeconds(duration))
                       join u in _dbContext.Users on cr.UserId equals u.Id                      
                       orderby cr.CreatedAt
                       select new ChatContent()
                       {
                           Name = u.Username,
                           Content = cr.Content
                       };
            return Ok(chat.ToList());
        }
    }
}
