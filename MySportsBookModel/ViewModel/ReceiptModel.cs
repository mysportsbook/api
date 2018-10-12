using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportsBookModel.ViewModel
{
    public class ReceiptModel : InvoiceModel
    {
        public int ReceiptId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public double AmountTobePaid { get; set; }
        public double OtherAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double AmountPaid { get; set; }
        public string Description { get; set; }
        public int PaymentId { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNumber { get; set; }
    }
}
