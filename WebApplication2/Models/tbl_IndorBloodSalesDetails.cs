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
    
    public partial class tbl_IndorBloodSalesDetails
    {
        public int IndorBloodSalesID { get; set; }
        public string IndorBloodsalesVoucherNo { get; set; }
        public int BloodID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public int MediUnitID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    
        public virtual tbl_BloodGroup tbl_BloodGroup { get; set; }
        public virtual tbl_IndorBloodSalesVoucher tbl_IndorBloodSalesVoucher { get; set; }
        public virtual tbl_MedicinUnit tbl_MedicinUnit { get; set; }
    }
}