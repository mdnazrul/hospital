using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceIndorMedicineSalesModel
    {
        public List<tbl_IndorMedicineSalesVoucher> indorMedicineSalesVoucherTbl { get; set; }
        public List<tbl_IndorMedicineSalesDetails> idnorMedicineSalesDetailsTbl { get; set; }
    }
}