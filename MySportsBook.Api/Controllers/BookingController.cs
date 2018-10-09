using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    public class BookingController : BaseController
    {
        [HttpGet]
        [Authorize]
        // GET api/booking
        public IHttpActionResult Get()
        {
            return GetResult();
        }

        [HttpGet]
        [Authorize]
        // GET api/booking
        public IHttpActionResult Get(int venueid)
        {
            return GetResult(venueid);
        }

        [HttpPost]
        [Authorize]
        // GET api/booking
        public IHttpActionResult Post(string booking)
        {
            return Ok(true);
        }

        [NonAction]
        private IHttpActionResult GetResult(int venueid = 0)
        {
            return Ok(true);
        }
    }
}
