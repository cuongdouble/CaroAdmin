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

        public virtual Result<User> Update(User model)
        {
            if (model == null)
                return Error<User>();
            var user = _dbContext.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if (user == null)
                return Error<User>($"User with id = {model.Id} not found.");
            if (model.BannedAt == null)
                user.BannedAt = DateTime.Now;
            else
                user.BannedAt = null;
            _dbContext.SaveChanges();
            return Ok(user);
        }
    }
}
