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
    
    public partial class HR_AttendanceBonus
    {
        public int AttendanceBouns_Id { get; set; }
        public string OfficeCode { get; set; }
        public Nullable<int> AttendanceDays1 { get; set; }
        public Nullable<int> AttendanceDays2 { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> Amount1 { get; set; }
        public Nullable<decimal> Amount2 { get; set; }
        public string AddBy { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
    }
}