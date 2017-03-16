using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class InvoiceController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string InvoiceVoucherNo = "";
        public static int invoiceid = 0;
        const int RecordsPerPage = 3;
        public ActionResult addInvoice()
        {
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.ServiceId = new SelectList(db.tbl_Service, "ServiceId", "ServiceName");
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");

            int vc;
            var MaxValue = (from item in db.tbl_InvoiceVocher select item.InvoiceVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                InvoiceVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                InvoiceVoucherNo = "0000000001";
            }
            ViewBag.InvoiceVoucherNo = InvoiceVoucherNo;
            return View();
        }
        public JsonResult InvoiceProcess(tbl_InvoiceVocher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_InvoiceVocher select item.InvoiceVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                InvoiceVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                InvoiceVoucherNo = "0000000001";
            }

            ViewBag.InvoiceVoucherNo = InvoiceVoucherNo;
            string userid = "";
            string userSettings = "";
            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["UserName"] != null)
                { userSettings = Request.Cookies["UserSettings"]["UserName"]; userid = Request.Cookies["UserSettings"]["UserId"]; }
            }
            o.EntryDate = DateTime.Today;
            o.UserID = Convert.ToInt32(userid);

            if (ModelState.IsValid)
            {
                tbl_InvoiceVocher fc = new tbl_InvoiceVocher { InvoiceVoucherNo = InvoiceVoucherNo, PatientID = o.PatientID, ReleaseDate = o.ReleaseDate, TotalAmount = o.TotalAmount, vat = o.vat, Discount = o.Discount, GrandTotalAmount = o.GrandTotalAmount, Remarks = o.Remarks, UserID = o.UserID, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate };
                foreach (var i in o.tbl_InvoiceDetails)
                {
                    fc.tbl_InvoiceDetails.Add(i);
                }

                db.tbl_InvoiceVocher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            var jsonData = new { success = true, status = status, InvoiceVoucherNo = InvoiceVoucherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //.................list................
        public ActionResult Invoicelist(tbl_InvoiceVocher invoice)
        {
            var results = (from item in db.tbl_InvoiceVocher
                           select item).ToList().OrderBy(p => p.InvoiceVoucherNo);
            var pageIndex = invoice.Page ?? 1;
            invoice.InvoicelistResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(invoice);
            
        }
        [HttpPost]
        public ActionResult Invoicelist(string keyword, tbl_InvoiceVocher invoice)
        {
            var results = (from item in db.tbl_InvoiceVocher where item.tbl_PatientInfo.PatientName.Contains(keyword) select item).ToList();
            var pageIndex = invoice.Page ?? 1;
            invoice.InvoicelistResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(invoice);
        }
        //............................Upadate......................
        [HttpGet]
        public ActionResult InvoiceEdit(tbl_InvoiceVocher tbv, string InvoiceVoucherNo, ReferenceInvoiceModel model)
        {
            ViewBag.ServiceId = new SelectList(db.tbl_Service, "ServiceId", "ServiceName");
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            ViewBag.InvoiceVoucherNo = InvoiceVoucherNo;
            model.InvoiceVoucherTbl = (from item in db.tbl_InvoiceVocher where item.InvoiceVoucherNo == InvoiceVoucherNo select item).ToList();
            model.InvoiceDeltailsTbl = (from item in db.tbl_InvoiceDetails where item.InvoiceVoucherNo == InvoiceVoucherNo select item).ToList();
            return View(model);
        }
        public JsonResult IvoiceUpdate(tbl_InvoiceVocher tbv, tbl_InvoiceDetails tbvd, string InvoiceVoucherNo, int PatientID, DateTime ReleaseDate, decimal TotalAmount, decimal vat, decimal Discount, decimal GrandTotalAmount, int InvoiceID, int ServiceId, float Amount)
        {
            tbv = db.tbl_InvoiceVocher.FirstOrDefault(x => x.InvoiceVoucherNo == InvoiceVoucherNo);
            tbv.PatientID = PatientID;
            tbv.ReleaseDate = ReleaseDate;
            tbv.TotalAmount = TotalAmount;
            tbv.vat = vat;
            tbv.Discount = Discount;
            tbv.GrandTotalAmount = GrandTotalAmount;
            db.SaveChanges();

            tbvd = db.tbl_InvoiceDetails.FirstOrDefault(x => x.InvoiceID == InvoiceID);
            tbvd.ServiceId = ServiceId;
            tbvd.Amount = Amount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult InvoiceDelete(InvoiceVocher tbv, InvoiceDetails tbvd, string InvoiceVoucherNo, int InvoiceID, decimal TotalAmount)
        //{
        //    tbvd = db.InvoiceDetails.FirstOrDefault(x => x.InvoiceID == InvoiceID);
        //    db.InvoiceDetails.Remove(tbvd);
        //    db.SaveChanges();

        //    tbv = db.InvoiceVochers.FirstOrDefault(x => x.InvoiceVoucherNo == InvoiceVoucherNo);
        //    //tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

        //    //tbv.vat = 0;
        //    //tbv.Discount = tbv.GrandTotalAmount;
        //    tbv.TotalAmount = tbv.TotalAmount - TotalAmount;

        //    //tbv.vat = 0;
        //    //tbv.Discount = tbv.TotalAmount;

        //    db.SaveChanges();

        //    var jsonData = new { success = true, message = "Successfully Deleted." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //............Delete...........
        public ActionResult Delete(tbl_InvoiceVocher tbv, tbl_InvoiceDetails tbvd, string InvoiceVoucherNo)
        {
            var info = (from item in db.tbl_InvoiceDetails where item.InvoiceVoucherNo == InvoiceVoucherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_InvoiceDetails.Remove(vp);
            }
            else
            {
                if (InvoiceVoucherNo != null)
                {
                    tbvd = db.tbl_InvoiceDetails.FirstOrDefault(x => x.InvoiceVoucherNo == InvoiceVoucherNo);
                    db.tbl_InvoiceDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();

            tbv = db.tbl_InvoiceVocher.FirstOrDefault(x => x.InvoiceVoucherNo == InvoiceVoucherNo);
            db.tbl_InvoiceVocher.Remove(tbv);
            db.SaveChanges();

            return RedirectToAction("Invoicelist");
        }
	}
}