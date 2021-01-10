using GomokuAdmin.Data;
using GomokuAdmin.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GomokuAdmin.Web.Services
{
    public class UserService : ServiceBase
    {
        private postgresContext _dbContext;
        public UserService()
        {
            _dbContext = new postgresContext();
        }

        public virtual Result<List<User>> Search(string term = null)
        {
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                term = term.Trim();

                var result =
                    _dbContext.Users
                    .Where(x =>
                        x.Username.ToLower().Contains(term) ||
                        x.Name.ToLower().Contains(term)
                    )
                    .ToList();

                return Ok(result);
            }

            return Ok(_dbContext.Users.ToList());
        }

        //public bool BanChat(Guid userId)
        //{
        //    _dbContext.Users.Find(userId).BanChat = true;
        //    return true;
        //}
    }
}
