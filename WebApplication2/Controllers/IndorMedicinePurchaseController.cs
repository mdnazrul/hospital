using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class IndorMedicinePurchaseController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string MedicinePurchaseVoucherNo = "";
        const int RecordsPerPage = 3;
        public static int medicinePurchaseId = 0;
        public ActionResult addIndorMedicinePurchase()
        {
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.MediInfoID = new SelectList(db.tbl_MedicineInfo, "MediInfoID", "MediName");
            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            int vc;
            var MaxValue = (from item in db.tbl_IndorMedicenPurshaseVoucher select item.MedicinePurchaseVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                MedicinePurchaseVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                MedicinePurchaseVoucherNo = "0000000001";
            }
            ViewBag.MedicinePurchaseVoucherNo = MedicinePurchaseVoucherNo;
            return View();
        }
        public JsonResult indorMedicinePurchaseProcess(tbl_IndorMedicenPurshaseVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_IndorMedicenPurshaseVoucher select item.MedicinePurchaseVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                MedicinePurchaseVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                MedicinePurchaseVoucherNo = "0000000001";
            }

            ViewBag.MedicinePurchaseVoucherNo = MedicinePurchaseVoucherNo;
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
                tbl_IndorMedicenPurshaseVoucher fc = new tbl_IndorMedicenPurshaseVoucher { MedicinePurchaseVoucherNo = MedicinePurchaseVoucherNo, MediSupplyerID = o.MediSupplyerID, PurchaseDate = o.PurchaseDate, VoucharCopyImage = o.VoucharCopyImage, GrandTotalAmount = o.GrandTotalAmount, PayAmount = o.PayAmount, DueAmount = o.DueAmount, UserID = o.UserID, ExperDate = o.ExperDate, Remarks = o.Remarks, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate };
                foreach (var i in o.tbl_IndorMedicinePurchaseDetails)
                {
                    fc.tbl_IndorMedicinePurchaseDetails.Add(i);
                }

                db.tbl_IndorMedicenPurshaseVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            var jsonData = new { success = true, status = status, MedicinePurchaseVoucherNo = MedicinePurchaseVoucherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        //...........List..............
        public ActionResult IndorMedicinePurchaseList(tbl_IndorMedicenPurshaseVoucher medicine)
        {
            var results = (from item in db.tbl_IndorMedicenPurshaseVoucher
                           select item).ToList().OrderBy(p => p.MedicinePurchaseVoucherNo);
            var pageIndex = medicine.Page ?? 1;
            medicine.IndorMedicinePurchaseListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(medicine);

            //return View(db.IndorMedicenPurshaseVouchers.ToList());
        }

        [HttpPost]
        public ActionResult IndorMedicinePurchaseList(string keyword, tbl_IndorMedicenPurshaseVoucher medicine)
        {

            var results = (from item in db.tbl_IndorMedicenPurshaseVoucher where item.tbl_MedicineSupplierInfo.MediSupplyerName.Contains(keyword) select item).ToList();
            var pageIndex = medicine.Page ?? 1;
            medicine.IndorMedicinePurchaseListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(medicine);

        }

        //............................Upadate......................
        [HttpGet]
        public ActionResult IndorMedicinePurchaseEdit(tbl_IndorMedicenPurshaseVoucher tbv, string MedicinePurchaseVoucherNo, ReferenceIndorMedicinePurchase model)
        {
            ViewBag.MediInfoID = new SelectList(db.tbl_MedicineInfo, "MediInfoID", "MediName");
            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            ViewBag.MedicinePurchaseVoucherNo = MedicinePurchaseVoucherNo;
            model.indorMedicinePurchaseVocherTbl = (from item in db.tbl_IndorMedicenPurshaseVoucher where item.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo select item).ToList();
            model.indorMedicinePurchaseDetailsTbl = (from item in db.tbl_IndorMedicinePurchaseDetails where item.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo select item).ToList();
            return View(model);
        }
        public JsonResult IndorMedicinePurchaseUpdate(tbl_IndorMedicenPurshaseVoucher tbv, tbl_IndorMedicinePurchaseDetails tbvd, string MedicinePurchaseVoucherNo, int MediSupplyerID, DateTime PurchaseDate, decimal GrandTotalAmount, decimal PayAmount, decimal DueAmount, int MedicinePurchaseId, int MediInfoID, decimal Rate, int Quantity, decimal Amount)
        {
            tbv = db.tbl_IndorMedicenPurshaseVoucher.FirstOrDefault(x => x.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo);
            tbv.MediSupplyerID = MediSupplyerID;
            tbv.PurchaseDate = PurchaseDate;
            tbv.GrandTotalAmount = GrandTotalAmount;
            tbv.PayAmount = PayAmount;
            tbv.DueAmount = DueAmount;
            db.SaveChanges();

            tbvd = db.tbl_IndorMedicinePurchaseDetails.FirstOrDefault(x => x.MedicinePurchaseId == MedicinePurchaseId);
            tbvd.MediInfoID = MediInfoID;
            tbvd.Rate = Rate;
            tbvd.Quantity = Quantity;
            tbvd.Amount = Amount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndormedicinePurchaseDelete(tbl_IndorMedicenPurshaseVoucher tbv, tbl_IndorMedicinePurchaseDetails tbvd, string MedicinePurchaseVoucherNo, int MedicinePurchaseId, decimal GrandTotalAmount)
        {
            tbvd = db.tbl_IndorMedicinePurchaseDetails.FirstOrDefault(x => x.MedicinePurchaseId == MedicinePurchaseId);
            db.tbl_IndorMedicinePurchaseDetails.Remove(tbvd);
            db.SaveChanges();

            tbv = db.tbl_IndorMedicenPurshaseVoucher.FirstOrDefault(x => x.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo);
            tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

            tbv.PayAmount = 0;
            tbv.DueAmount = tbv.GrandTotalAmount;
            db.SaveChanges();

            var jsonData = new { success = true, message = "Successfully Deleted." };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //............................Delete.........................
        public ActionResult Delete(tbl_IndorMedicenPurshaseVoucher tbv, tbl_IndorMedicinePurchaseDetails tbvd, string MedicinePurchaseVoucherNo)
        {
            var info = (from item in db.tbl_IndorMedicinePurchaseDetails where item.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_IndorMedicinePurchaseDetails.Remove(vp);
            }
            else
            {
                if (MedicinePurchaseVoucherNo != null)
                {
                    tbvd = db.tbl_IndorMedicinePurchaseDetails.FirstOrDefault(x => x.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo);
                    db.tbl_IndorMedicinePurchaseDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();

            tbv = db.tbl_IndorMedicenPurshaseVoucher.FirstOrDefault(x => x.MedicinePurchaseVoucherNo == MedicinePurchaseVoucherNo);
            db.tbl_IndorMedicenPurshaseVoucher.Remove(tbv);
            db.SaveChanges();

            return RedirectToAction("IndorMedicinePurchaseList");
        }
	}
}