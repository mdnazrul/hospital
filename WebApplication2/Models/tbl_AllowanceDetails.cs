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
    
    public partial class tbl_AllowanceDetails
    {
        public int AllowanceDetailId { get; set; }
        public string AllowanceVocherNo { get; set; }
        public int AllowanceID { get; set; }
        public Nullable<decimal> AllowanceAmount { get; set; }
    
        public virtual tbl_AllowanceType tbl_AllowanceType { get; set; }
        public virtual tbl_AllowanceVoucher tbl_AllowanceVoucher { get; set; }
    }
}
