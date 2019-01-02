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
    public class EnquiryController : BaseController
    {
        [HttpGet]
        [Authorize]
        // GET api/enquiry
        public IHttpActionResult Get()
        {
            return GetResult();
        }

        [HttpGet]
        [Authorize]
        // GET api/enquiry
        public IHttpActionResult Get(int venueid)
        {
            return GetResult(venueid);
        }

        [HttpPost]
        [Authorize]
        // GET api/enquiry
        public IHttpActionResult Post(EnquiryModel enquiryModel)
        {
            Master_Enquiry _enquiry = new Master_Enquiry();
            if (enquiryModel.EnquiryId == 0)
            {
                _enquiry = new Master_Enquiry() { FK_VenueId = enquiryModel.VenueId, Game = enquiryModel.Game, Mobile = enquiryModel.Mobile, Name = enquiryModel.Name, Slot = enquiryModel.Slot, Comments = enquiryModel.Comment };
                _enquiry.CreatedBy = CurrentUser.PK_UserId;
                _enquiry.CreatedDate = DateTime.Now.ToLocalTime();
                dbContext.Master_Enquiry.Add(_enquiry);
            }
            else
            {
                _enquiry = dbContext.Master_Enquiry.Find(enquiryModel.EnquiryId);
                if (_enquiry != null)
                {
                    _enquiry.Name = enquiryModel.Name;
                    _enquiry.Game = enquiryModel.Game;
                    _enquiry.Mobile = enquiryModel.Mobile;
                    _enquiry.Slot = enquiryModel.Slot;
                    dbContext.Entry(_enquiry).State = EntityState.Modified;
                }
            }
            dbContext.SaveChanges();
            if (enquiryModel.Comments != null && enquiryModel.Comments.Any())
            {
                enquiryModel.Comments.ToList().ForEach(c =>
                {
                    dbContext.Transaction_Enquiry_Comments.Add(new Transaction_Enquiry_Comments()
                    {
                        Comments = c,
                        FK_EnquiryId = _enquiry.PK_EnquiryId,
                        CreatedBy = CurrentUser.PK_UserId,
                        CreatedDate = DateTime.Now.ToLocalTime()
                    });
                });

                dbContext.SaveChanges();
            }
            return Ok(enquiryModel);
        }

        [NonAction]
        private IHttpActionResult GetResult(int venueid = 0)
        {
            var enquiryModel = dbContext.Master_Enquiry.Where(x => x.PK_EnquiryId == (venueid != 0 ? venueid : x.PK_EnquiryId))
                 .Select(e =>
                          new EnquiryModel
                          {
                              EnquiryId = e.PK_EnquiryId,
                              Game = e.Game,
                              Mobile = e.Mobile,
                              Name = e.Name,
                              Slot = e.Slot,
                              Comment = e.Comments,
                              VenueId = e.FK_VenueId,

                          }).ToList();

            enquiryModel.ForEach(x =>
            {
                var _item = dbContext.Transaction_Enquiry_Comments.Where(c => c.FK_EnquiryId == x.EnquiryId).OrderByDescending(o => o.CreatedDate).AsEnumerable().Select(com => $"{com.CreatedDate.ToString("dd/MM/yyyy")} - {com.Comments.ToString()}".ToString()).ToList();
                if (_item != null)
                {
                    x.Comments = new List<string>();
                    x.Comments.AddRange(_item);
                }
            });
            return Ok(enquiryModel);
        }
    }
}
