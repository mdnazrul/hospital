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
    
    public partial class tbl_MedicineInfo
    {
        public tbl_MedicineInfo()
        {
            this.tbl_IndorMedicinePurchaseDetails = new HashSet<tbl_IndorMedicinePurchaseDetails>();
            this.tbl_IndorMedicineSalesDetails = new HashSet<tbl_IndorMedicineSalesDetails>();
            this.tbl_OutdorMedicineSalesDetails = new HashSet<tbl_OutdorMedicineSalesDetails>();
            this.tbl_OutdorMedicineSalesDetails1 = new HashSet<tbl_OutdorMedicineSalesDetails>();
        }
    
        public int MediInfoID { get; set; }
        public string MediName { get; set; }
        public string MedicompaName { get; set; }
        public int MediUnitID { get; set; }
        public int MediCataID { get; set; }
    
        public virtual ICollection<tbl_IndorMedicinePurchaseDetails> tbl_IndorMedicinePurchaseDetails { get; set; }
        public virtual ICollection<tbl_IndorMedicineSalesDetails> tbl_IndorMedicineSalesDetails { get; set; }
        public virtual tbl_MedicinCatagory tbl_MedicinCatagory { get; set; }
        public virtual tbl_MedicinUnit tbl_MedicinUnit { get; set; }
        public virtual ICollection<tbl_OutdorMedicineSalesDetails> tbl_OutdorMedicineSalesDetails { get; set; }
        public virtual ICollection<tbl_OutdorMedicineSalesDetails> tbl_OutdorMedicineSalesDetails1 { get; set; }
    }
}
