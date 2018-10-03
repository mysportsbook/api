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
        // GET api/attendance
        public IHttpActionResult Get()
        {
            return GetResult();
        }

        [HttpGet]
        [Authorize]
        // GET api/attendance
        public IHttpActionResult Get(int venueid)
        {
            return GetResult(venueid);
        }

        [HttpPost]
        [Authorize]
        // GET api/venue
        public IHttpActionResult Post(EnquiryModel enquiryModel)
        {
            enquiryModel.Enquiry.CreatedBy = CurrentUser.PK_UserId;
            enquiryModel.Enquiry.CreatedDate = DateTime.Now.ToUniversalTime();

            if (enquiryModel.Enquiry.Id == 0)
                dbContext.Enquiries.Add(enquiryModel.Enquiry);
            else
                dbContext.Entry(enquiryModel.Enquiry).State = EntityState.Modified;
            dbContext.SaveChanges();
            enquiryModel.Enquiry_Comments.ForEach(c =>
            {
                c.EnquiryId = enquiryModel.Enquiry.Id;
                c.CreatedBy = CurrentUser.PK_UserId;
                c.CreatedDate = DateTime.Now.ToUniversalTime();
            });
            dbContext.Enquiry_Comments.AddRange(enquiryModel.Enquiry_Comments);
            dbContext.SaveChanges();
            return Ok(enquiryModel);
        }

        [NonAction]
        private IHttpActionResult GetResult(int venueid = 0)
        {
            List<EnquiryModel> enquiryModel = new List<EnquiryModel>();
            dbContext.Enquiries.Where(x => x.Id == (venueid != 0 ? venueid : x.Id)).ToList().ForEach(enq =>
            {
                enquiryModel.Add(new EnquiryModel()
                {
                    Enquiry = enq,
                    Enquiry_Comments = dbContext.Enquiry_Comments.Where(c => c.EnquiryId == enq.Id).OrderByDescending(x => x.CreatedDate).ToList()
                });
            });
            return Ok(enquiryModel);
        }
    }
}
