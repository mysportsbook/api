using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    public class PlayerAttendanceController : BaseController
    {
        [HttpGet]
        [Authorize]
        // GET api/playerAttendance
        public IHttpActionResult Get(int venueid, int sportid)
        {
            PlayerController playerController = new PlayerController();
            return Ok(playerController.GetPlayer(venueid, sportid, 0, 0).ToList().Where(b => b.IsAttendanceRequired).ToList().GroupBy(x => x.PlayerId).Select(p => p.First()));
        }
    }
}
