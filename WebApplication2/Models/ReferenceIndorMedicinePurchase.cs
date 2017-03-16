using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceIndorMedicinePurchase
    {
        public List<tbl_IndorMedicenPurshaseVoucher> indorMedicinePurchaseVocherTbl { get; set; }
        public List<tbl_IndorMedicinePurchaseDetails> indorMedicinePurchaseDetailsTbl { get; set; }
    }
}