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
    
    public partial class HR_Shift
    {
        public int ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftName { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<System.TimeSpan> LateAllowed { get; set; }
        public string Weekend1 { get; set; }
        public string Weekend2 { get; set; }
        public string ShiftType { get; set; }
        public string ShiftRemarks { get; set; }
        public Nullable<System.TimeSpan> TotalShiftHour { get; set; }
        public string AddBy { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public string EditBy { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
    }
}