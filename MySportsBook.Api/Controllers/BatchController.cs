using MySportsBookModel;
using MySportsBookModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    [RoutePrefix("api/Batch")]
    public class BatchController : BaseController
    {
        // GET api/batch
        [HttpGet]
        public IHttpActionResult Get()
        {

            return Ok((from bc in dbContext.BatchCounts
                       select new BatchCountModel()
                       {
                           BatchCountId = bc.PK_BatchCount,
                           BatchId = bc.Master_Batch.PK_BatchId,
                           BatchName = bc.Master_Batch.BatchName,
                           CourtId = bc.Master_Court.PK_CourtId,
                           CourtName = bc.Master_Court.CourtName,
                           StartTime = bc.Master_Batch.StartTime.ToString(),
                           EndTime = bc.Master_Batch.EndTime.ToString(),
                           Count = bc.Count
                       }).ToList());
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int venueid)
        {

            return GetResult(venueid);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int venueid, int sportid)
        {
            return GetResult(venueid, sportid);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int venueid, int sportid, int courtid)
        {

            return GetResult(venueid, sportid, courtid);
        }


        [NonAction]
        public IHttpActionResult GetResult(int venueid, int sportid = 0, int courtid = 0)
        {
            return Ok(dbContext.Master_Batch.Where(b => b.FK_VenueId == venueid && b.FK_StatusId == 1)
                      .Join(dbContext.Master_Court.Where(c => c.FK_StatusId == 1 && c.PK_CourtId == (courtid != 0 ? courtid : c.PK_CourtId)), batch => batch.FK_CourtId, court => court.PK_CourtId, (batch, court) => new { batch, court })
                      .Join(dbContext.Master_Sport.Where(s => s.FK_StatusId == 1 && s.PK_SportId == (sportid != 0 ? sportid : s.PK_SportId)), batcou => batcou.court.FK_SportId, sport => sport.PK_SportId, (batcou, sport) => new { batcou, sport })
                      .Join(dbContext.Master_Venue.Where(v => v.FK_StatusId == 1), batcouspo => batcouspo.batcou.batch.FK_VenueId, venue => venue.PK_VenueId, (batcouspo, venue) => new { batcouspo, venue })
                      .Select(b =>
                             new BatchModel()
                             {
                                 VenueId = b.venue.PK_VenueId,
                                 VenueCode = b.venue.VenueCode,
                                 VenueName = b.venue.VenueName,
                                 SportId = b.batcouspo.sport.PK_SportId,
                                 SportCode = b.batcouspo.sport.SportCode,
                                 SportName = b.batcouspo.sport.SportName,
                                 CourtId = b.batcouspo.batcou.court.PK_CourtId,
                                 CourtCode = b.batcouspo.batcou.court.CourtCode,
                                 CourtName = b.batcouspo.batcou.court.CourtName,
                                 BatchId = b.batcouspo.batcou.batch.PK_BatchId,
                                 BatchCode = b.batcouspo.batcou.batch.BatchCode,
                                 BatchName = b.batcouspo.batcou.batch.BatchName,
                                 StartTime = b.batcouspo.batcou.batch.StartTime,
                                 EndTime = b.batcouspo.batcou.batch.EndTime,
                                 MaxPlayer = b.batcouspo.batcou.batch.MaxPlayers,
                                 PlayerCount = dbContext.Transaction_PlayerSport.Where(s => s.FK_BatchId == b.batcouspo.batcou.batch.PK_BatchId && s.FK_StatusId == 1).Count(),
                                 IsAttendanceRequired = b.batcouspo.batcou.batch.IsAttendanceRequired
                             }));
        }


    }
}