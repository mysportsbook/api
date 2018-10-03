﻿using MySportsBookModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySportsBookModel;

namespace MySportsBook.Api.Controllers
{
    [RoutePrefix("api/attendance")]
    public class AttendanceController : BaseController
    {

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid)
        {
            return GetResult(venueid);
        }

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid, int sportid)
        {
            return GetResult(venueid, sportid);
        }

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid, int sportid, int courtid)
        {
            return GetResult(venueid, sportid, courtid);
        }

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid, int sportid, int courtid, int batchid)
        {
            return GetResult(venueid, sportid, courtid, batchid);
        }

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid, int sportid, int courtid, int batchid, int playerid)
        {
            return GetResult(venueid, sportid, courtid, batchid, playerid);
        }

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid, int sportid, int courtid, int batchid, int playerid, DateTime? date)
        {
            return GetResult(venueid, sportid, courtid, batchid, playerid, date);
        }

        [HttpPost]
        [Authorize]
        // POST api/attendance
        public IHttpActionResult Post(List<AttendanceModel> attendanceModel)
        {
            List<Transaction_Attendance> attendanceModels = new List<Transaction_Attendance>();
            attendanceModel.ForEach(a =>
            {
                attendanceModels.Add(new Transaction_Attendance()
                {
                    FK_VenueId = a.VenueId,
                    FK_PlayerId = a.PlayerId,
                    FK_BatchId = a.BatchId,
                    Date = a.Date.Value,
                    Present = true,
                    CreatedBy = CurrentUser.PK_UserId,
                    CreatedDate = DateTime.Now.ToUniversalTime()

                });
            });
            var model = attendanceModel.FirstOrDefault();
            dbContext.Transaction_Attendance.RemoveRange(dbContext.Transaction_Attendance.Where(x => x.FK_VenueId == model.VenueId && x.FK_BatchId == model.BatchId && x.Date == model.Date).AsEnumerable());
            dbContext.Transaction_Attendance.AddRange(attendanceModels);
            dbContext.SaveChanges();
            return Ok(attendanceModels);
        }

        [NonAction]
        public IHttpActionResult GetResult(int venueid, int sportid = 0, int courtid = 0, int batchid = 0, int playerid = 0, DateTime? date = null)
        {
            //Get the attendance for the particular batch
            var allattendance = dbContext.Master_Player.Where(p => p.FK_VenueId == venueid && p.FK_StatusId == 1 && p.PK_PlayerId == (playerid != 0 ? playerid : p.PK_PlayerId))
                       .Join(dbContext.Transaction_PlayerSport.Where(ps => ps.FK_StatusId == 1), player => player.PK_PlayerId, playsport => playsport.FK_PlayerId, (player, playsport) => new { player, playsport })
                       .Join(dbContext.Master_Batch.Where(b => b.FK_StatusId == 1 && b.PK_BatchId == (batchid != 0 ? batchid : b.PK_BatchId)), playersport => playersport.playsport.FK_BatchId, batch => batch.PK_BatchId, (playersport, batch) => new { playersport, batch })
                       .Join(dbContext.Master_Court.Where(c => c.FK_StatusId == 1 && c.PK_CourtId == (courtid != 0 ? courtid : c.PK_CourtId)), playspobat => playspobat.batch.FK_CourtId, court => court.PK_CourtId, (playspobat, court) => new { playspobat, court })
                       .Join(dbContext.Master_Sport.Where(s => s.FK_StatusId == 1 && s.PK_SportId == (sportid != 0 ? sportid : s.PK_SportId)), playspobatcou => playspobatcou.court.FK_SportId, sport => sport.PK_SportId, (playspobatcou, sport) => new { playspobatcou, sport })
                       .Join(dbContext.Master_Venue.Where(v => v.FK_StatusId == 1), playspobatcouspo => playspobatcouspo.playspobatcou.playspobat.batch.FK_VenueId, venue => venue.PK_VenueId, (playspobatcou, venue) => new { playspobatcou, venue })
                       .GroupJoin(dbContext.Transaction_Attendance.Where(a => a.FK_VenueId == venueid && a.Date == (date != null ? date : a.Date)).DefaultIfEmpty(),
                                    allvenue => new { batchid = allvenue.playspobatcou.playspobatcou.playspobat.batch.PK_BatchId, playerid = allvenue.playspobatcou.playspobatcou.playspobat.playersport.player.PK_PlayerId },
                                    attend => new { batchid = attend.FK_BatchId, playerid = attend.FK_PlayerId }, (allvenue, attend) => new { allvenue, attend })
                        .SelectMany(p =>
                            p.attend.DefaultIfEmpty(), (all, att) => new { allvenue = all, attend = att })
                        .Select(a =>

                         new AttendanceModel
                         {
                             VenueId = a.allvenue.allvenue.venue.PK_VenueId,
                             VenueCode = a.allvenue.allvenue.venue.VenueCode,
                             VenueName = a.allvenue.allvenue.venue.VenueName,
                             SportId = a.allvenue.allvenue.playspobatcou.sport.PK_SportId,
                             SportCode = a.allvenue.allvenue.playspobatcou.sport.SportCode,
                             SportName = a.allvenue.allvenue.playspobatcou.sport.SportName,
                             CourtId = a.allvenue.allvenue.playspobatcou.playspobatcou.court.PK_CourtId,
                             CourtCode = a.allvenue.allvenue.playspobatcou.playspobatcou.court.CourtCode,
                             CourtName = a.allvenue.allvenue.playspobatcou.playspobatcou.court.CourtName,
                             BatchId = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.PK_BatchId,
                             BatchCode = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.BatchCode,
                             BatchName = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.BatchName,
                             StartTime = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.StartTime,
                             EndTime = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.EndTime,
                             MaxPlayer = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.MaxPlayers,
                             PlayerCount = dbContext.Transaction_PlayerSport.Where(s => s.FK_BatchId == a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.batch.PK_BatchId && s.FK_StatusId == 1).Count(),
                             PlayerId = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.playersport.player.PK_PlayerId,
                             FirstName = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.playersport.player.FirstName,
                             LastName = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.playersport.player.LastName,
                             Email = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.playersport.player.Email,
                             Mobile = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.playersport.player.Mobile,
                             PlayerSportId = a.allvenue.allvenue.playspobatcou.playspobatcou.playspobat.playersport.playsport.PK_PlayerSportId,
                             AttendanceId = (a.attend != null ? a.attend.PK_AttendanceId : default(int)),
                             Present = (a.attend != null ? a.attend.Present : default(bool)),
                             Date = (a.attend != null ? a.attend.Date : default(DateTime))
                         });
            //Add the player from other batch
            var attendance = dbContext.Transaction_Attendance.Where(a => a.FK_VenueId == venueid && a.Date == (date != null ? date : a.Date) && a.FK_BatchId == batchid)
                                .Join(dbContext.Master_Player.Where(p => p.FK_VenueId == venueid && p.FK_StatusId == 1), att => att.FK_PlayerId, play => play.PK_PlayerId, (att, play) => new { att, play });
            if (attendance.Count() != allattendance.Count())
            {
                attendance.ToList().ForEach(att =>
                {
                    if (allattendance.Where(a => a.PlayerId == att.att.FK_PlayerId).Count() <= 0)
                    {
                        allattendance.ToList().Add(new AttendanceModel()
                        {
                            PlayerId = att.att.FK_PlayerId,
                            AttendanceId = att.att.PK_AttendanceId,
                            Present = true,
                            Date = date,
                            FirstName = att.play.FirstName,
                            LastName = att.play.LastName,
                            Email = att.play.Email,
                            Mobile = att.play.Mobile,
                        });
                    }
                });
            }

            return Ok(allattendance);
        }
    }
}


