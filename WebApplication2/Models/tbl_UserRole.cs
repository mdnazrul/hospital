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
    
    public partial class tbl_UserRole
    {
        public tbl_UserRole()
        {
            this.tbl_UserInfo = new HashSet<tbl_UserInfo>();
        }
    
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
        public string Remarks { get; set; }
    
        public virtual ICollection<tbl_UserInfo> tbl_UserInfo { get; set; }
    }
}
