using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MySportsBookModel;

namespace MySportsBook.Api.Controllers
{
    public class BaseController : ApiController
    {
        public MySportsBookEntities dbContext;
        public Configuration_User CurrentUser;
        public Master_UserRole CurrentRole;
        public Master_Venue CurrentVenue;

        public BaseController()
        {
            CurrentUser = new Configuration_User();
            CurrentRole = new Master_UserRole();
            CurrentVenue = new Master_Venue();
            dbContext = new MySportsBookEntities();
            var user = User;
            if (user != null)
            {
                CurrentUser = dbContext.Configuration_User.ToList().Find(u => u.UserName.ToLower().Equals(user.Identity.Name.ToLower()) || u.Email.ToLower().Equals(user.Identity.Name.ToLower()) || u.Mobile.ToLower().Equals(user.Identity.Name.ToLower()));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}