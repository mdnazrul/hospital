using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceOutdoorBloodSalesModel
    {
        public List<tbl_OutdorBloodSalesVoucher> OutDoorBloodSalesVoucherTbl { get; set; }
        public List<tbl_OutdorBloodSalesDetails> OutdoorbloodSalesDeltailsTbl { get; set; }
    }
}