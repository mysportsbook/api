using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class InvoiceModel : PlayerModel
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double TotalFee { get; set; }
        public double TotalOtherAmount { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalPaidAmount { get; set; }
        public double ExtraPaidAmount { get; set; }
        public string Comments { get; set; }
        public string TransactionNo { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int ReceivedBy { get; set; }
        public bool NoDues { get; set; }
        public List<InvoiceDetailModel> InvoiceDetails { get; set; }
    }
    public class InvoiceDetailModel : BatchModel
    {
        public int InvoiceId { get; set; }
        public int InvoiceDetailssId { get; set; }
        public int InvoicePeriodId { get; set; }
        public string InvoicePeriod { get; set; }
        public double Fee { get; set; }
        public int PayOrder { get; set; }
        public double PaidAmount { get; set; }
        public int StatusId { get; set; }
        public int PlayerId { get; set; }
        public string Comments { get; set; }
    }
}
