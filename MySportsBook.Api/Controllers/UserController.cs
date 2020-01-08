using MySportsBook.Common;
using MySportsBookModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace MySportsBook.Api.Controllers
{
    public class UserController : ApiController
    {
        public MySportsBookEntities dbContext;
        // GET api/user
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/user/Login")]
        // GET api/user/Login
        public bool Login()
        {

            return true;
        }

        [NonAction]
        public bool Login(string username, string password)
        {
            if (dbContext == null)
                dbContext = new MySportsBookEntities();
            var _user = dbContext.Configuration_User.ToList().Find(u => u.UserName.ToLower().Equals(username.ToLower()) || u.Email.ToLower().Equals(username.ToLower()) || u.Mobile.ToLower().Equals(username.ToLower()));
            if (_user != null)
            {
                return Cryptography.Verify(_user.PasswordSalt, _user.PasswordHash, password);
            }
            return false;
        }
    }
}