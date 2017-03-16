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
    
    public partial class tbl_Leave
    {
        public int LeaveID { get; set; }
        public int StaffID { get; set; }
        public Nullable<System.DateTime> LeaveDateFrom { get; set; }
        public Nullable<System.DateTime> LeaveDateTo { get; set; }
        public Nullable<int> TotalLeaveDays { get; set; }
        public string LeaveCause { get; set; }
        public Nullable<int> TotalLeave { get; set; }
        public string Remarks { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual tbl_staffInfo tbl_staffInfo { get; set; }
        public virtual tbl_UserInfo tbl_UserInfo { get; set; }
    }
}
