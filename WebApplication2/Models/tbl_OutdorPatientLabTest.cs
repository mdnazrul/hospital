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
    
    public partial class tbl_OutdorPatientLabTest
    {
        public int OutdorLabTestId { get; set; }
        public string OutdorName { get; set; }
        public string Address { get; set; }
        public string MobNo { get; set; }
        public int LabID { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual tbl_LabInfo tbl_LabInfo { get; set; }
        public virtual tbl_UserInfo tbl_UserInfo { get; set; }
    }
}
