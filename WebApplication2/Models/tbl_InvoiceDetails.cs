//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_InvoiceDetails
    {
        public int InvoiceID { get; set; }
        public string InvoiceVoucherNo { get; set; }
        public int ServiceId { get; set; }
        public Nullable<double> Amount { get; set; }
    
        public virtual tbl_InvoiceVocher tbl_InvoiceVocher { get; set; }
        public virtual tbl_Service tbl_Service { get; set; }
    }
}