using MySportsBookModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using MySportsBookModel;
using MySportsBook.Common.Helper;

namespace MySportsBook.Api.Controllers
{
    public class InvoiceController : BaseController
    {
        [HttpGet]
        [Authorize]
        // GET api/invoice
        public IHttpActionResult Get(int venueid)
        {
            return Ok(GetAllInvoice(venueid));
        }

        [HttpGet]
        [Authorize]
        // GET api/invoice
        public IHttpActionResult Get(int venueid, int sportid, int courtid, int batchid, int playerid)
        {
            return GetResult(venueid, sportid, courtid, batchid, playerid);
        }

        [HttpPost]
        [Authorize]
        // GET api/invoice
        public IHttpActionResult Post(InvoiceModel invoice)
        {
            if (invoice.ShouldClose)
                Close(invoice);
            else
                return Save(invoice);
            return Ok("Error");
        }

        [NonAction]
        public IHttpActionResult GetResult(int venueid, int sportid, int courtid, int batchid, int playerid)
        {
            return Ok(GetInvoiceDetails(venueid, sportid, courtid, batchid, playerid));
        }

        [NonAction]
        public List<InvoiceDetailsModel> GetInvoiceDetails(int venueid, int sportid, int courtid, int batchid, int playerid)
        {
            List<InvoiceDetailsModel> invoiceDetails = new List<InvoiceDetailsModel>();
            var batchdetails = dbContext.Transaction_PlayerSport.Include(c => c.Master_Sport).Include(c => c.Master_Batch).Where(x => x.FK_StatusId == 1 && x.FK_PlayerId == playerid);
            int _months = 0, _freq = 0, _totalmonths = 0;
            string[] _listMonths = new string[] { };
            DateTime _date;
            batchdetails.ToList().ForEach(batch =>
            {
                _date = DateTime.ParseExact(batch.LastGeneratedMonth, "MMMyyyy", CultureInfo.CurrentCulture);
                _months = ((DateTime.Now.Year - _date.Year) * 12) + (DateTime.Now.Month - _date.Month);
                if (_months > 0)
                {
                    _freq = batch.FK_InvoicePeriodId == 1 ? 1 : batch.FK_InvoicePeriodId == 2 ? 3 : batch.FK_InvoicePeriodId == 3 ? 6 : 12;
                    _totalmonths = (int)Math.Ceiling((decimal)(_months / (decimal)_freq)) * _freq;
                    _listMonths = Enumerable.Range(0, Int32.MaxValue)
                     .Select(e => batch.CreatedDate.AddMonths(e))
                     .TakeWhile(e => e <= batch.CreatedDate.AddMonths((int)_totalmonths).ToUniversalTime())
                     .Select(e => e.ToString("MMMyyyy").ToUpper()).ToArray();
                    for (int count = 0; count < _totalmonths; count += _freq)
                    {
                        invoiceDetails.Add(new InvoiceDetailsModel()
                        {
                            BatchId = batch.FK_BatchId,
                            SportName = batch.Master_Sport.SportName,
                            Fee = (double)batch.Master_Batch.Fee * _freq,
                            InvoicePeriod = (batch.FK_InvoicePeriodId == 1 ? _listMonths[count] : _listMonths[count] + "-" + _listMonths[count + (_freq - 1)]),
                            InvoicePeriodId = batch.FK_InvoicePeriodId
                        });
                    }
                }

            });
            return invoiceDetails;
        }

        [NonAction]
        public IQueryable<InvoiceModel> GetAllInvoice(int venueid)
        {
            List<InvoiceModel> invoiceModel = new List<InvoiceModel>();
            var _invoices = dbContext.Transaction_Invoice.Where(x => x.FK_StatusId == 1 && x.FK_VenueId == venueid)
                            .Join(dbContext.Master_Player.Where(x => x.FK_StatusId == 1 && x.FK_VenueId == venueid), invoice => invoice.FK_PlayerId, player => player.PK_PlayerId, (invoice, player) => new { invoice, player });
            _invoices.ToList().ForEach(inv =>
            {
                invoiceModel.Add(new InvoiceModel
                {
                    InvoiceId = inv.invoice.PK_InvoiceId,
                    InvoiceDate = inv.invoice.InvoiceDate,
                    InvoiceNumber = inv.invoice.InvoiceNumber,
                    FirstName = $"{inv.player.FirstName}-{inv.player.Mobile}",
                    TotalFee = (double)inv.invoice.TotalFee,
                    TotalDiscount = (double)inv.invoice.TotalDiscount,
                    LateFee = (double)inv.invoice.LateFee,
                    PaidAmount = (double)inv.invoice.PaidAmount,
                    Comments = inv.invoice.Comments,
                    invoiceDetails = dbContext.Transaction_InvoiceDetail.Where(x => x.FK_StatusId == 1 && x.FK_InvoiceId == inv.invoice.PK_InvoiceId)
                                     .Join(dbContext.Master_Batch.Where(x => x.FK_StatusId == 1 && x.FK_VenueId == venueid), invdetails => invdetails.FK_BatchId, batch => batch.PK_BatchId, (invdetails, batch) => new { invdetails, batch })
                                     .Join(dbContext.Master_Court.Where(x => x.FK_StatusId == 1 && x.FK_VenueId == venueid), batchinv => batchinv.batch.FK_CourtId, court => court.PK_CourtId, (batchinv, court) => new { batchinv, court })
                                     .Join(dbContext.Master_Sport.Where(x => x.FK_StatusId == 1 && x.FK_VenueId == venueid), batchinvcourt => batchinvcourt.court.FK_SportId, sport => sport.PK_SportId, (batchinvcourt, sport) => new { batchinvcourt, sport })
                    .Select(details =>
                        new InvoiceDetailsModel()
                        {
                            InvoicePeriod = details.batchinvcourt.batchinv.invdetails.InvoicePeriod,
                            Fee = (double)details.batchinvcourt.batchinv.invdetails.Amount,
                            BatchId = details.batchinvcourt.batchinv.invdetails.FK_BatchId,
                            BatchName = details.batchinvcourt.batchinv.batch.BatchName,
                            SportName = details.sport.SportName
                        }).ToList()


                });
            });
            return invoiceModel.AsQueryable();
        }

        [NonAction]
        public IHttpActionResult Save(InvoiceModel invoice)
        {
            var _playerinvoice = dbContext.Transaction_Invoice.Where(inv => inv.FK_StatusId.Equals(3) && inv.FK_VenueId == invoice.VenueId && inv.FK_PlayerId == invoice.PlayerId)
                   .Join(dbContext.Transaction_InvoiceDetail.Where(detail => detail.FK_StatusId.Equals(3)), inv => inv.PK_InvoiceId, detail => detail.FK_InvoiceId, (inv, detail) => new { inv, detail }).ToList();
            if (invoice.invoiceDetails.Count > 0)
            {
                var _shouldupdate = false;
                if (_playerinvoice != null && _playerinvoice.Count > 0)
                {
                    invoice.invoiceDetails.ForEach(d =>
                    {
                        if (!_shouldupdate)
                            _shouldupdate = _playerinvoice.Any(p => p.detail.InvoicePeriod.Equals(d.InvoicePeriod));
                    });
                }
                if (_shouldupdate)
                    Update(invoice);
                else
                {
                    foreach (var item in invoice.invoiceDetails)
                    {
                        if (!CheckforPreviousInvoice(invoice.PlayerId, item.BatchId, item.InvoicePeriod, item.InvoicePeriodId))
                            return Ok("Frist pay pending invoice for previous month");
                    }
                    var _transInvoice = new Transaction_Invoice()
                    {
                        FK_VenueId = invoice.VenueId,
                        FK_PlayerId = invoice.PlayerId,
                        FK_StatusId = ((invoice.TotalFee + invoice.LateFee - invoice.TotalDiscount) <= invoice.PaidAmount) ? 4 : 3,
                        InvoiceDate = DateTime.Now.ToUniversalTime(),
                        InvoiceNumber = NumberGenerateHelper.GenerateInvoiceNo(),
                        DueDate = DateTime.Now.AddDays(10).ToUniversalTime(),
                        TotalFee = (decimal)invoice.TotalFee,
                        TotalDiscount = (decimal)invoice.TotalDiscount,
                        LateFee = (decimal)invoice.LateFee,
                        PaidAmount = (decimal)invoice.PaidAmount,
                        Comments = invoice.Comments,
                        CreatedBy = CurrentUser.PK_UserId,
                        CreatedDate = DateTime.Now.ToUniversalTime()
                    };
                    dbContext.Transaction_Invoice.Add(_transInvoice);
                    dbContext.SaveChanges();
                    invoice.InvoiceId = _transInvoice.PK_InvoiceId;
                    invoice.invoiceDetails.ForEach(d =>
                    {
                        SaveDetail(_transInvoice.PK_InvoiceId, invoice.PlayerId, invoice.Comments, _transInvoice.FK_StatusId, d);
                    });
                    dbContext.SaveChanges();
                    invoice.invoiceDetails.ForEach(d =>
                    {
                        UpdateLastInv(_transInvoice.PK_InvoiceId, invoice.PlayerId, d.BatchId, d.InvoicePeriod);
                    });
                    //Generate Receipt
                    var _invoice = dbContext.Transaction_Invoice.Find(_transInvoice.PK_InvoiceId);
                    dbContext.Transaction_Receipt.Add(new Transaction_Receipt()
                    {
                        PK_ReceiptId = 0,
                        ReceiptNumber = NumberGenerateHelper.GenerateInvoiceNo(),
                        ReceiptDate = DateTime.Now.ToUniversalTime(),
                        AmountTobePaid = _invoice.TotalFee,
                        OtherAmount = _invoice.LateFee,
                        DiscountAmount = _invoice.TotalDiscount,
                        AmountPaid = (decimal)_invoice.PaidAmount,
                        FK_InvoiceId = _invoice.PK_InvoiceId,
                        FK_PaymentModeId = invoice.PaymentId,
                        FK_StatusId = 1,
                        FK_VenueId = invoice.VenueId,
                        Description = invoice.Comments,
                        CreatedBy = CurrentUser.PK_UserId,
                        CreatedDate = DateTime.Now.ToUniversalTime()
                    });
                    dbContext.SaveChanges();
                }
                if (invoice.InvoiceId != 0)
                {
                    //if (dbContext.Transaction_InvoiceDetail.All(d => d.FK_InvoiceId == invoice.InvoiceId && d.FK_StatusId == 4) && dbContext.Transaction_InvoiceDetail.Where(d => d.FK_InvoiceId == invoice.InvoiceId && d.FK_StatusId != 3).Sum(s=>s.Amount))
                    //{
                    //    var _invoice = dbContext.Transaction_Invoice.Find(invoice.InvoiceId);
                    //    _invoice.FK_StatusId = 4;
                    //    _invoice.ModifiedBy = CurrentUser.PK_UserId;
                    //    _invoice.ModifiedDate = DateTime.Now.ToUniversalTime();
                    //    dbContext.Entry(_invoice).State = EntityState.Modified;
                    //    dbContext.SaveChanges();
                    //}
                }
                return Ok(invoice);
            }
            return Ok("Nothing to save");
        }

        [NonAction]
        public void Update(InvoiceModel invoice)
        {
            //TODO:UPdate the Invoice
        }

        [NonAction]
        public void Close(InvoiceModel invoice)
        {
            if (invoice.InvoiceId == 0)
            {
                var _transInvoice = new Transaction_Invoice()
                {
                    FK_VenueId = invoice.VenueId,
                    FK_PlayerId = invoice.PlayerId,
                    FK_StatusId = 4,
                    InvoiceDate = invoice.InvoiceDate,
                    InvoiceNumber = NumberGenerateHelper.GenerateInvoiceNo(),
                    DueDate = invoice.InvoiceDate,
                    TotalFee = (decimal)invoice.TotalFee,
                    TotalDiscount = (decimal)invoice.TotalDiscount,
                    LateFee = (decimal)invoice.LateFee,
                    PaidAmount = (decimal)invoice.PaidAmount,
                    Comments = invoice.Comments,
                    CreatedBy = CurrentUser.PK_UserId,
                };
                dbContext.Transaction_Invoice.Add(_transInvoice);
                dbContext.SaveChanges();
                invoice.InvoiceId = _transInvoice.PK_InvoiceId;
                invoice.invoiceDetails.ForEach(d =>
                {
                    SaveDetail(_transInvoice.PK_InvoiceId, invoice.PlayerId, invoice.Comments, 4, d);
                });
                dbContext.SaveChanges();
                invoice.invoiceDetails.ForEach(d =>
                {
                    UpdateLastInv(_transInvoice.PK_InvoiceId, invoice.PlayerId, d.BatchId, d.InvoicePeriod);
                });

            }
            else
            {
                invoice.invoiceDetails.ForEach(d =>
                {
                    var _details = dbContext.Transaction_InvoiceDetail.Find(d.InvoiceDetailssId);
                    if (_details != null)
                    {
                        _details.Comments = invoice.Comments;
                        _details.FK_StatusId = 4;
                        _details.ModifiedBy = CurrentUser.PK_UserId;
                        _details.ModifiedDate = DateTime.Now.ToUniversalTime();
                        dbContext.Entry(_details).State = EntityState.Modified;
                    }
                    else
                    {
                        SaveDetail(invoice.InvoiceId, invoice.PlayerId, invoice.Comments, 4, d);
                    }
                });
                dbContext.SaveChanges();
                invoice.invoiceDetails.ForEach(d =>
                {
                    UpdateLastInv(invoice.InvoiceId, invoice.PlayerId, d.BatchId, d.InvoicePeriod);
                });
            }
            if (invoice.InvoiceId != 0)
            {
                if (dbContext.Transaction_InvoiceDetail.All(d => d.FK_InvoiceId == invoice.InvoiceId && d.FK_StatusId == 4))
                {
                    var _invoice = dbContext.Transaction_Invoice.Find(invoice.InvoiceId);
                    _invoice.FK_StatusId = 4;
                    _invoice.ModifiedBy = CurrentUser.PK_UserId;
                    _invoice.ModifiedDate = DateTime.Now.ToUniversalTime();
                    dbContext.Entry(_invoice).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            dbContext.SaveChanges();
        }


        [NonAction]
        public void SaveDetail(int invoiceid, int playerid, string comments, int statusid, InvoiceDetailsModel detail)
        {
            dbContext.Transaction_InvoiceDetail.Add(new Transaction_InvoiceDetail()
            {
                FK_BatchId = detail.BatchId,
                FK_InvoiceId = invoiceid,
                FK_StatusId = statusid,
                Amount = (decimal)detail.Fee,
                InvoicePeriod = detail.InvoicePeriod,
                Comments = comments,
                CreatedBy = CurrentUser.PK_UserId,
                CreatedDate = DateTime.Now.ToUniversalTime()
            });

        }
        [NonAction]
        public void UpdateLastInv(int invoiceid, int playerid, int BatchId, string InvoicePeriod)
        {
            var _playersport = dbContext.Transaction_PlayerSport.Where(s => s.FK_PlayerId == playerid && s.FK_BatchId == BatchId).FirstOrDefault();
            if (_playersport != null)
            {
                _playersport.LastGeneratedMonth = InvoicePeriod.IndexOf('-') > 0 ? InvoicePeriod.Split('-')[1].ToString().Trim() : InvoicePeriod;
                _playersport.ModifiedBy = CurrentUser.PK_UserId;
                _playersport.ModifiedDate = DateTime.Now.ToUniversalTime();
                dbContext.Entry(_playersport).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
        }
        [NonAction]
        public bool CheckforPreviousInvoice(int playerid, int batchid, string invperiod, int invperiodid)
        {
            //TODO:Need to check if all the previous invoice are generated
            var _playersport = dbContext.Transaction_PlayerSport.Where(s => s.FK_PlayerId == playerid && s.FK_BatchId == batchid).FirstOrDefault();
            if (invperiodid == 1)
            {
                var _currdate = DateTime.ParseExact(invperiod, "MMMyyyy", CultureInfo.CurrentCulture).AddMonths(-1);
                if (dbContext.Transaction_Invoice.Where(i => i.FK_PlayerId == playerid && i.FK_StatusId == 1)
                    .Join(dbContext.Transaction_InvoiceDetail.Where(d => d.InvoicePeriod == _currdate.ToString("MMMyyyy")), invoice => invoice.PK_InvoiceId, details => details.FK_InvoiceId, (invoice, details) => new { invoice, details })
                    .Any())
                    return true;
            }

            return true;
        }


    }
}
