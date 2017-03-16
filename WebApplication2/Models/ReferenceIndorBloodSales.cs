using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceIndorBloodSales
    {
        public List<tbl_IndorBloodSalesVoucher> indorBloodSalesVocherTbl { get; set; }
        public List<tbl_IndorBloodSalesDetails> indorBloodSalesDetailsTbl { get; set; }
    }
}