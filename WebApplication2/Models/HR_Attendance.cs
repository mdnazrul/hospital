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
    
    public partial class HR_Attendance
    {
        public int AttendanceId { get; set; }
        public string EID { get; set; }
        public string ShiftCode { get; set; }
        public Nullable<System.DateTime> Attendance_Date { get; set; }
        public string Attendance_Day { get; set; }
        public string Working_DayType { get; set; }
        public Nullable<System.TimeSpan> In_Time { get; set; }
        public Nullable<System.TimeSpan> Out_Time { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string AttendanceStatus { get; set; }
        public Nullable<System.TimeSpan> Over_Time { get; set; }
        public Nullable<System.TimeSpan> Worked_Time { get; set; }
        public Nullable<System.TimeSpan> ShiftTotalTime { get; set; }
        public Nullable<bool> OT_Status { get; set; }
        public Nullable<int> OT_Hour { get; set; }
        public Nullable<int> OT_Minute { get; set; }
        public Nullable<int> OT_ExtraAdd { get; set; }
        public Nullable<int> OT_Deduction { get; set; }
        public Nullable<int> OT_Total { get; set; }
        public Nullable<bool> Update_Status { get; set; }
        public Nullable<bool> ManualAttendanceStatus { get; set; }
        public string AttendanceRemarks { get; set; }
        public string LeaveCode { get; set; }
        public string AddBy { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public string EditBy { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
    }
}
