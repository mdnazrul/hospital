using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceAllowanceModel
    {
        public List<tbl_AllowanceVoucher> AllowanceVoucherTbl { get; set; }
        public List<tbl_AllowanceDetails> AllowanceDeltailsTbl { get; set; }
    }
}