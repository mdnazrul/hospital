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
    
    public partial class tbl_BloodGroup
    {
        public tbl_BloodGroup()
        {
            this.tbl_BloodDonerInfo = new HashSet<tbl_BloodDonerInfo>();
            this.tbl_BloodPurchaseeDetails = new HashSet<tbl_BloodPurchaseeDetails>();
            this.tbl_IndorBloodSalesDetails = new HashSet<tbl_IndorBloodSalesDetails>();
            this.tbl_OutdorBloodSalesDetails = new HashSet<tbl_OutdorBloodSalesDetails>();
        }
    
        public int BloodID { get; set; }
        public string BloodName { get; set; }
        public string Remarks { get; set; }
    
        public virtual ICollection<tbl_BloodDonerInfo> tbl_BloodDonerInfo { get; set; }
        public virtual ICollection<tbl_BloodPurchaseeDetails> tbl_BloodPurchaseeDetails { get; set; }
        public virtual ICollection<tbl_IndorBloodSalesDetails> tbl_IndorBloodSalesDetails { get; set; }
        public virtual ICollection<tbl_OutdorBloodSalesDetails> tbl_OutdorBloodSalesDetails { get; set; }
    }
}
