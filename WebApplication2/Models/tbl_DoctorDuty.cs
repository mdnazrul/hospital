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
    
    public partial class tbl_DoctorDuty
    {
        public int DoctorDutyID { get; set; }
        public int StaffID { get; set; }
        public string Shift { get; set; }
        public string VisitTime { get; set; }
        public string VisitDescription { get; set; }
        public string OperationSchedule { get; set; }
        public string OutDorScheduleTime { get; set; }
        public string OperationTime { get; set; }
        public string OperationDescription { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
    
        public virtual tbl_staffInfo tbl_staffInfo { get; set; }
    }
}