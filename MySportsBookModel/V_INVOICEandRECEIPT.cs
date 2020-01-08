//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySportsBookModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_INVOICEandRECEIPT
    {
        public int Receipt_ReceiptId { get; set; }
        public string Receipt_ReceiptNumber { get; set; }
        public System.DateTime Receipt_ReceiptDate { get; set; }
        public int Receipt_VenueId { get; set; }
        public int Receipt_InvoiceId { get; set; }
        public decimal Receipt_TotalFee { get; set; }
        public Nullable<decimal> Receipt_TotalOtherAmount { get; set; }
        public Nullable<decimal> Receipt_TotalDiscountAmount { get; set; }
        public decimal Receipt_AmountPaid { get; set; }
        public string Receipt_Description { get; set; }
        public int Receipt_PaymentModeId { get; set; }
        public string Receipt_TransactionNumber { get; set; }
        public int Receipt_StatusId { get; set; }
        public Nullable<System.DateTime> Receipt_TransactionDate { get; set; }
        public int Receipt_ReceivedBy { get; set; }
        public int Invoice_InvoiceId { get; set; }
        public string Invoice_InvoiceNumber { get; set; }
        public int Invoice_VenueId { get; set; }
        public int Invoice_PlayerId { get; set; }
        public System.DateTime Invoice_InvoiceDate { get; set; }
        public System.DateTime Invoice_DueDate { get; set; }
        public decimal Invoice_TotalFee { get; set; }
        public Nullable<decimal> Invoice_TotalDiscount { get; set; }
        public Nullable<decimal> Invoice_OtherAmount { get; set; }
        public Nullable<decimal> Invoice_PaidAmount { get; set; }
        public string Invoice_Comments { get; set; }
        public int InvoiceDetail_InvoiceDetailId { get; set; }
        public int InvoiceDetail_InvoiceId { get; set; }
        public int InvoiceDetail_BatchId { get; set; }
        public string InvoiceDetail_InvoicePeriod { get; set; }
        public decimal InvoiceDetail_BatchAmount { get; set; }
        public string InvoiceDetail_Comments { get; set; }
        public decimal InvoiceDetail_PaidAmount { get; set; }
    }
}
