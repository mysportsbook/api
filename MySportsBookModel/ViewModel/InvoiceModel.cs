using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class InvoiceModel : PlayerModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double TotalFee { get; set; }
        public double TotalDiscount { get; set; }
        public double LateFee { get; set; }
        public double PaidAmount { get; set; }
        public string Comments { get; set; }
        public bool ShouldClose { get; set; }
        public bool GenerateReceipt { get; set; }
        public int PaymentId { get; set; }
        public List<InvoiceDetailsModel> invoiceDetails { get; set; }
    }
    public class InvoiceDetailsModel : BatchModel
    {
        public string InvoicePeriod { get; set; }
        public double Fee { get; set; }
    }
}
