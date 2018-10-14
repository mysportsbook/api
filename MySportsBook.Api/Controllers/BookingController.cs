using MySportsBookModel;
using MySportsBookModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    public class BookingController : BaseController
    {
        List<BookingModel> bookingModels = new List<BookingModel>();
        List<Tuple<int, string, string>> allslots = new List<Tuple<int, string, string>>();
        [HttpGet]
        [Authorize]
        // GET api/booking
        public IHttpActionResult Get()
        {
            return GetResult(string.Empty);
        }

        [HttpGet]
        [Authorize]
        // GET api/booking
        public IHttpActionResult Get(int venueid)
        {
            return GetResult(string.Empty);
        }

        [HttpPost]
        [Authorize]
        // GET api/booking
        public IHttpActionResult Post(int venueid, [FromBody]string booking)
        {
            return GetResult(booking, venueid);
        }

        [NonAction]
        private IHttpActionResult GetResult(string booking, int venueid = 0)
        {
            if (!string.IsNullOrEmpty(booking))
                Process(booking.Split(','), venueid);
            return Ok(true);
        }
        [NonAction]
        private void Process(string[] message, int venueid)
        {
            bool IsCancel = false, processlots = false;
            int count = 0;
            string bookingno, name, mobile, court, venue, amount, date, slot, value, mode = "Online";
            bookingno = name = mobile = court = venue = amount = date = slot = value = string.Empty;

            message.ToList().ForEach(item =>
            {
                value = string.Empty;
                try { value = GetValue(item.Trim(), IsCancel); } catch { value = string.Empty; }
                if (item.Trim().ToUpper().StartsWith("CANCEL") || item.Trim().ToUpper().StartsWith("BOOK"))
                {
                    bookingno = value;
                    IsCancel = item.Trim().ToUpper().StartsWith("CANCEL");
                }
                else if (item.Trim().ToUpper().StartsWith("NAME"))
                    name = value;
                else if (item.Trim().ToUpper().StartsWith("M") && !item.Trim().ToUpper().StartsWith("MODE"))
                {
                    mobile = value;
                    processlots = false;
                }
                else if (item.Trim().ToUpper().StartsWith("COURT"))
                {
                    court = value;
                    processlots = false;
                }
                else if (item.Trim().ToUpper().StartsWith("VENUE") || item.Trim().ToUpper().StartsWith("GROUND"))
                {
                    venue = value;
                    processlots = false;
                }
                else if (item.Trim().ToUpper().StartsWith("AMOUNT"))
                {
                    amount = value;
                    processlots = false;
                }
                else if (item.Trim().ToUpper().StartsWith("MODE"))
                {
                    mode = value;
                    processlots = false;
                }
                else if (item.Trim().ToUpper().StartsWith("SLOTS"))
                {
                    date = value;
                    processlots = true;
                }
                else if (processlots && !string.IsNullOrEmpty(item.Trim()))
                {
                    if (count % 2 == 0)
                        slot = item.Trim();
                    else
                        date = item.Trim();
                    count += 1;
                }
                else if (processlots && string.IsNullOrEmpty(item.Trim()))
                    processlots = false;
                if (!string.IsNullOrEmpty(date.Trim()) && !string.IsNullOrEmpty(slot.Trim()))
                {
                    allslots.Add(new Tuple<int, string, string>(count, date, slot));
                    slot = date = string.Empty;
                }
            });
            var booking = dbContext.OtherBookings.Where(b => b.BookingNo == bookingno).FirstOrDefault();
            var _model = new OtherBooking
            {
                FK_VenueId = venueid,
                BookingNo = bookingno,
                Name = name,
                Mobile = mobile,
                Mode = mode,
                Amount = amount,
                Message = string.Join(",", message).ToString(),
                CreatedBy = CurrentUser.PK_UserId,
                CreatedDate = DateTime.Now.ToUniversalTime()

            };
            if (IsCancel && booking != null)
            {
                allslots.ToList().ForEach(slots =>
                {
                    var detail = dbContext.OtherBookingDetails.Where(d => d.Date == slots.Item2 && d.Slot == slots.Item3)
                                .Join(dbContext.OtherBookings.Where(b => b.BookingNo == bookingno), details => details.FK_BookingId, book => book.PK_BookingId, (details, book) => new { details, book })
                                .Select(a => new
                                {
                                    a.details
                                }).FirstOrDefault();
                    if (detail != null)
                    {
                        detail.details.Status = "Cancelled";
                        detail.details.ModifiedBy = CurrentUser.PK_UserId;
                        detail.details.ModifiedDate = DateTime.Now.ToUniversalTime();
                        SaveDetails(detail.details);
                    }
                    else
                    {
                        SaveDetails(new OtherBookingDetail
                        {
                            FK_VenueId = venueid,
                            Court = court,
                            Date = slots.Item2,
                            Slot = slots.Item3,
                            Status = IsCancel ? "Cancelled" : "Booked",
                            FK_BookingId = booking.PK_BookingId,
                            CreatedBy = CurrentUser.PK_UserId,
                            CreatedDate = DateTime.Now.ToUniversalTime()
                        });
                    }
                });
            }
            else
            {
                if (booking != null)
                {
                    dbContext.OtherBookingDetails.RemoveRange(dbContext.OtherBookingDetails.Where(x => x.FK_BookingId == booking.PK_BookingId));
                    dbContext.SaveChanges();
                    dbContext.OtherBookings.Remove(booking);
                    dbContext.SaveChanges();
                }
                Save(_model, court, IsCancel, venueid);
            }
        }

        [NonAction]
        private string GetValue(string item, bool IsCancel)
        {
            int _index, _Length;
            string _msg;
            if (item.ToUpper().StartsWith("CANCEL"))
                item = item.Remove(0, item.IndexOf(':') + 1).Trim();
            _index = item.IndexOf(':') + 1;
            _Length = item.Length;
            _msg = item.Trim().Substring(_index, _Length - _index).Trim();
            if (item.Trim().ToUpper().StartsWith("COURT") && _msg.ToUpper().IndexOf("GROUND") > 0)
                return _msg.Remove(_msg.ToUpper().IndexOf("GROUND"), _msg.Length - _msg.ToUpper().IndexOf("GROUND")).Trim();
            return _msg;
        }
        [NonAction]
        private void Save(OtherBooking otherBooking, string court, bool IsCancel, int venueid)
        {
            dbContext.OtherBookings.Add(otherBooking);
            dbContext.SaveChanges();
            allslots.ToList().ForEach(slots =>
            {
                SaveDetails(new OtherBookingDetail
                {
                    FK_VenueId = venueid,
                    Court = court,
                    Date = slots.Item2,
                    Slot = slots.Item3,
                    Status = IsCancel ? "Cancelled" : "Booked",
                    FK_BookingId = otherBooking.PK_BookingId,
                    CreatedBy = CurrentUser.PK_UserId,
                    CreatedDate = DateTime.Now.ToUniversalTime()
                });

            });
        }
        [NonAction]
        private void SaveDetails(OtherBookingDetail otherBookingDetail)
        {
            if (otherBookingDetail.PK_BookingDetailId == 0)
                dbContext.OtherBookingDetails.Add(otherBookingDetail);
            else
                dbContext.Entry(otherBookingDetail).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
