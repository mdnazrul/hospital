using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceBloodPurchaseModel
    {
        public List<tbl_BloodPurchaseVoucher> VoucherTbl { get; set; }
        public List<tbl_BloodPurchaseeDetails> VoucherDetailsTbl { get; set; }
    }
}