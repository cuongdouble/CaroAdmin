using GomokuAdmin.Data;
using GomokuAdmin.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GomokuAdmin.Web.Services
{
    public class AccountService : ServiceBase
    {
        private postgresContext _dbContext;
        public AccountService()
        {
            _dbContext = new postgresContext();
        }

        public Result<ServiceUser> Login(HttpContext context, string login, string password)
        {
            var account = _dbContext.Admins.Where(x => x.Username == login).FirstOrDefault();
            if (account == null)
                return Error<ServiceUser>("Username not correct");
            if(account.Password!= password)
                return Error<ServiceUser>("Password not correct");
            context.Response.Cookies.Append(Constants.AuthorizationCookieKey, login);

            return Ok(new ServiceUser
            {
                Login = login
            });
        }

        public Result<ServiceUser> Verify(HttpContext context)
        {
            var cookieValue = context.Request.Cookies[Constants.AuthorizationCookieKey];
            if (string.IsNullOrEmpty(cookieValue))
                return Error<ServiceUser>();
            return Ok(new ServiceUser
            {
                Login = cookieValue
            });
        }

        public Result Logout(HttpContext context)
        {
            context.Response.Cookies.Delete(Constants.AuthorizationCookieKey);
            return Ok();
        }
    }
}