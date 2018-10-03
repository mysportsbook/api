using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MySportsBookModel;
using MySportsBookModel.ViewModel;

namespace MySportsBook.Api.Controllers
{
    public class VenueController : BaseController
    {
        [Authorize]
        // GET api/venue
        public IHttpActionResult Get()
        {
            return Ok(dbContext.Master_UserVenue.Where(x => x.FK_UserId == CurrentUser.PK_UserId)
                        .Join(dbContext.Master_Venue, uservenue => uservenue.FK_VenueId, venue => venue.PK_VenueId, (uservenue, venue) => new { uservenue, venue })
                        .Select(v => new VenueModel()
                        {
                            VenueId = v.venue.PK_VenueId,
                            VenueCode = v.venue.VenueCode,
                            VenueName = v.venue.VenueName
                        }));

        }

        [HttpPost]
        [Authorize]
        // GET api/venue
        public IHttpActionResult Post(int venueid)
        {
            CurrentVenue.PK_VenueId = venueid;
            return Ok(CurrentVenue);
        }
    }
}