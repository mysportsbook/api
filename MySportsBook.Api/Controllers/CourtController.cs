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
    public class CourtController : BaseController
    {
        [Authorize]
        // GET: api/Court
        public IHttpActionResult GetCourt(int venueid)
        {
            return GetResult(venueid);
        }
        [Authorize]
        // GET: api/Court
        public IHttpActionResult GetCourt(int venueid, int sportid)
        {
            return GetResult(venueid, sportid);
        }

        [NonAction]
        public IHttpActionResult GetResult(int venueid, int sportid = 0)
        {
            return Ok(dbContext.Master_Court.Where(c => c.FK_VenueId == venueid && c.FK_SportId == (sportid != 0 ? sportid : c.FK_SportId) && c.FK_StatusId == 1)
                     .Join(dbContext.Master_Sport.Where(x => x.FK_VenueId == venueid && x.FK_StatusId == 1), court => court.FK_SportId, sport => sport.PK_SportId, (court, sport) => new { court, sport })
                     .Join(dbContext.Master_Venue.Where(v => v.FK_StatusId == 1), couspo => couspo.sport.FK_VenueId, venue => venue.PK_VenueId, (couspo, venue) => new { couspo, venue })
                     .Select(c =>
                      new CourtModel()
                      {
                          VenueId = c.venue.PK_VenueId,
                          VenueCode = c.venue.VenueCode,
                          VenueName = c.venue.VenueName,
                          SportId = c.couspo.sport.PK_SportId,
                          SportCode = c.couspo.sport.SportCode,
                          SportName = c.couspo.sport.SportName,
                          CourtId = c.couspo.court.PK_CourtId,
                          CourtCode = c.couspo.court.CourtCode,
                          CourtName = c.couspo.court.CourtName
                      }));
        }

    }
}