using MySportsBookModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    public class SportController : BaseController
    {
        [Authorize]
        // GET api/venue
        public IHttpActionResult Get(int venueid)
        {
            return GetResult(venueid);
        }

        [Authorize]
        // GET api/venue
        public IHttpActionResult Get(int venueid, int sportid)
        {
            return GetResult(venueid, sportid);
        }

        [NonAction]
        public IHttpActionResult GetResult(int venueid, int sportid = 0)
        {
            return Ok(dbContext.Master_Sport.Where(x => x.FK_VenueId == venueid && x.FK_StatusId == 1 && x.PK_SportId == (sportid != 0 ? sportid : x.PK_SportId))
                       .Join(dbContext.Master_Venue, sport => sport.FK_VenueId, venue => venue.PK_VenueId, (sport, venue) => new { sport, venue })
                       .Select(s =>
                         new SportModel()
                         {
                             VenueId = s.venue.PK_VenueId,
                             VenueCode = s.venue.VenueCode,
                             VenueName = s.venue.VenueName,
                             SportId = s.sport.PK_SportId,
                             SportCode = s.sport.SportCode,
                             SportName = s.sport.SportName
                         }));
        }
    }
}
