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
    
    public partial class tbl_StaffType
    {
        public tbl_StaffType()
        {
            this.tbl_staffInfo = new HashSet<tbl_staffInfo>();
        }
    
        public int StaffTypeID { get; set; }
        public string StaffName { get; set; }
        public string Remarks { get; set; }
    
        public virtual ICollection<tbl_staffInfo> tbl_staffInfo { get; set; }
    }
}