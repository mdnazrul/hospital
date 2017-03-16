using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ReferenceInvoiceModel
    {
        public List<tbl_InvoiceVocher> InvoiceVoucherTbl { get; set; }
        public List<tbl_InvoiceDetails> InvoiceDeltailsTbl { get; set; }
    }
}