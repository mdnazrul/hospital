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
    
    public partial class tbl_SpecializationType
    {
        public tbl_SpecializationType()
        {
            this.tbl_DoctorSpecialization = new HashSet<tbl_DoctorSpecialization>();
        }
    
        public int SpeciaTypeID { get; set; }
        public string SpecializationName { get; set; }
        public string Remarks { get; set; }
    
        public virtual ICollection<tbl_DoctorSpecialization> tbl_DoctorSpecialization { get; set; }
    }
}