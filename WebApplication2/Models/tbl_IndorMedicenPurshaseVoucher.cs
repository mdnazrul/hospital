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
    
    public partial class tbl_IndorMedicenPurshaseVoucher
    {
        public tbl_IndorMedicenPurshaseVoucher()
        {
            this.tbl_IndorMedicinePurchaseDetails = new HashSet<tbl_IndorMedicinePurchaseDetails>();
        }
    
        public string MedicinePurchaseVoucherNo { get; set; }
        public int MediSupplyerID { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public string VoucharCopyImage { get; set; }
        public Nullable<decimal> GrandTotalAmount { get; set; }
        public Nullable<decimal> PayAmount { get; set; }
        public Nullable<decimal> DueAmount { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> ExperDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual tbl_MedicineSupplierInfo tbl_MedicineSupplierInfo { get; set; }
        public virtual tbl_UserInfo tbl_UserInfo { get; set; }
        public virtual ICollection<tbl_IndorMedicinePurchaseDetails> tbl_IndorMedicinePurchaseDetails { get; set; }
    }
}
