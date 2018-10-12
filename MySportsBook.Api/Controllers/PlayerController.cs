using MySportsBookModel;
using MySportsBookModel.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    public class PlayerController : BaseController
    {

        [HttpGet]
        [Authorize]
        // GET api/player
        public IHttpActionResult Get(int venueid)
        {
            return Get(venueid, 0);
        }

        [HttpGet]
        [Authorize]
        // GET api/player
        public IHttpActionResult Get(int venueid, int sportid)
        {
            return Get(venueid, sportid, 0);
        }

        [HttpGet]
        [Authorize]
        // GET api/player
        public IHttpActionResult Get(int venueid, int sportid, int courtid)
        {
            var _result = GetPlayer(venueid, sportid, courtid).ToList().GroupBy(g => g.PlayerId).Select(p => p.First());
            return Ok(_result);
        }

        [HttpGet]
        [Authorize]
        // GET api/player
        public IHttpActionResult Get(int venueid, int sportid, int courtid, int batchid)
        {
            return GetResult(venueid, sportid, courtid, batchid);
        }



        [HttpGet]
        [Authorize]
        // GET api/player
        public IHttpActionResult Get(int venueid, int sportid, int courtid, int batchid, int playerid)
        {
            return GetResult(venueid, sportid, courtid, batchid, playerid);
        }

        [NonAction]
        public IHttpActionResult GetResult(int venueid, int sportid = 0, int courtid = 0, int batchid = 0, int playerid = 0)
        {
            return Ok(GetPlayer(venueid, sportid, courtid, batchid, playerid));
        }
        [NonAction]
        public IQueryable<PlayerModel> GetPlayer(int venueid, int sportid = 0, int courtid = 0, int batchid = 0, int playerid = 0)
        {
            return dbContext.Master_Player.Where(p => p.FK_VenueId == venueid && p.FK_StatusId == 1 && p.PK_PlayerId == (playerid != 0 ? playerid : p.PK_PlayerId))
                        .Join(dbContext.Transaction_PlayerSport.Where(ps => ps.FK_StatusId == 1), player => player.PK_PlayerId, playsport => playsport.FK_PlayerId, (player, playsport) => new { player, playsport })
                        .Join(dbContext.Master_Batch.Where(b => b.FK_StatusId == 1 && b.PK_BatchId == (batchid != 0 ? batchid : b.PK_BatchId)), playersport => playersport.playsport.FK_BatchId, batch => batch.PK_BatchId, (playersport, batch) => new { playersport, batch })
                        .Join(dbContext.Master_Court.Where(c => c.FK_StatusId == 1 && c.PK_CourtId == (courtid != 0 ? courtid : c.PK_CourtId)), playspobat => playspobat.batch.FK_CourtId, court => court.PK_CourtId, (playspobat, court) => new { playspobat, court })
                        .Join(dbContext.Master_Sport.Where(s => s.FK_StatusId == 1 && s.PK_SportId == (sportid != 0 ? sportid : s.PK_SportId)), playspobatcou => playspobatcou.court.FK_SportId, sport => sport.PK_SportId, (playspobatcou, sport) => new { playspobatcou, sport })
                        .Join(dbContext.Master_Venue.Where(v => v.FK_StatusId == 1), playspobatcouspo => playspobatcouspo.playspobatcou.playspobat.batch.FK_VenueId, venue => venue.PK_VenueId, (playspobatcou, venue) => new { playspobatcou, venue })
                        .Select(p =>
                          new PlayerModel
                          {
                              VenueId = p.venue.PK_VenueId,
                              VenueCode = p.venue.VenueCode,
                              VenueName = p.venue.VenueName,
                              SportId = p.playspobatcou.sport.PK_SportId,
                              SportCode = p.playspobatcou.sport.SportCode,
                              SportName = p.playspobatcou.sport.SportName,
                              CourtId = p.playspobatcou.playspobatcou.court.PK_CourtId,
                              CourtCode = p.playspobatcou.playspobatcou.court.CourtCode,
                              CourtName = p.playspobatcou.playspobatcou.court.CourtName,
                              BatchId = p.playspobatcou.playspobatcou.playspobat.batch.PK_BatchId,
                              BatchCode = p.playspobatcou.playspobatcou.playspobat.batch.BatchCode,
                              BatchName = p.playspobatcou.playspobatcou.playspobat.batch.BatchName,
                              StartTime = p.playspobatcou.playspobatcou.playspobat.batch.StartTime,
                              EndTime = p.playspobatcou.playspobatcou.playspobat.batch.EndTime,
                              MaxPlayer = p.playspobatcou.playspobatcou.playspobat.batch.MaxPlayers,
                              PlayerCount = dbContext.Transaction_PlayerSport.Where(s => s.FK_BatchId == p.playspobatcou.playspobatcou.playspobat.batch.PK_BatchId && s.FK_StatusId == 1).Count(),
                              PlayerId = p.playspobatcou.playspobatcou.playspobat.playersport.player.PK_PlayerId,
                              FirstName = p.playspobatcou.playspobatcou.playspobat.playersport.player.FirstName,
                              LastName = p.playspobatcou.playspobatcou.playspobat.playersport.player.LastName,
                              Email = p.playspobatcou.playspobatcou.playspobat.playersport.player.Email,
                              Mobile = p.playspobatcou.playspobatcou.playspobat.playersport.player.Mobile,
                              PlayerSportId = p.playspobatcou.playspobatcou.playspobat.playersport.playsport.PK_PlayerSportId,
                              IsAttendanceRequired = p.playspobatcou.playspobatcou.playspobat.batch.IsAttendanceRequired
                          });
        }


    }
}