using MySportsBookModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySportsBook.Api.Controllers
{
    public class PaymentModeController : BaseController
    {
        [HttpGet]
        [Authorize]
        // GET: api/PaymentMode
        public IHttpActionResult Get(int venueid = 0)
        {
            return GetResult(venueid);
        }

        [NonAction]
        public IHttpActionResult GetResult(int venueid)
        {
            return Ok(GetPaymentMode(venueid));
        }

        [NonAction]
        private IQueryable<PaymentModeModel> GetPaymentMode(int venueid)
        {
            return dbContext.Confirguration_PaymentMode.Select(mode => new PaymentModeModel
            {
                PayementId = mode.PK_PaymentModeId,
                PaymentCode = mode.PaymentModeCode,
                PaymentName = mode.PaymentMode
            });
        }
    }
}
