using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceOutDoorMedicineSalesModel
    {
        public List<tbl_OutdorMedicineSalesVoucher> OutdorMedicineSalesVoucherTbl { get; set; }
        public List<tbl_OutdorMedicineSalesDetails> OutdoorMedicinesalesDeltailsTbl { get; set; }
    }
}