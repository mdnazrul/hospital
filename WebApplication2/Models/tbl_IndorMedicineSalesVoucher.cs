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
    
    public partial class tbl_IndorMedicineSalesVoucher
    {
        public tbl_IndorMedicineSalesVoucher()
        {
            this.tbl_IndorMedicineSalesDetails = new HashSet<tbl_IndorMedicineSalesDetails>();
        }
    
        public string IndorMedicenSaleseVocherNo { get; set; }
        public int PatientID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> VatAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> GrandTotalAmount { get; set; }
        public Nullable<decimal> PayAmount { get; set; }
        public Nullable<decimal> DueAmount { get; set; }
        public Nullable<System.DateTime> PayDate { get; set; }
        public int UserInfoID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual ICollection<tbl_IndorMedicineSalesDetails> tbl_IndorMedicineSalesDetails { get; set; }
        public virtual tbl_PatientInfo tbl_PatientInfo { get; set; }
        public virtual tbl_UserInfo tbl_UserInfo { get; set; }
    }
}
