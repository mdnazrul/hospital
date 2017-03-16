using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class IndorMedicineSalesController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string IndorMedicenSaleseVocherNo = "";
        public static int indorMedicinesalesid = 0;
        const int RecordPerPages = 3;
        public ActionResult addIndormedicinesales()
        {
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.MediInfoID = new SelectList(db.tbl_MedicineInfo, "MediInfoID", "MediName");
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");

            int vc;
            var MaxValue = (from item in db.tbl_IndorMedicineSalesVoucher select item.IndorMedicenSaleseVocherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                IndorMedicenSaleseVocherNo = vc.ToString("0000000000");
            }
            else
            {
                IndorMedicenSaleseVocherNo = "0000000001";
            }
            ViewBag.IndorMedicenSaleseVocherNo = IndorMedicenSaleseVocherNo;
            return View();
        }
        public JsonResult indorMedicinesalesProcess(tbl_IndorMedicineSalesVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_IndorMedicineSalesVoucher select item.IndorMedicenSaleseVocherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                IndorMedicenSaleseVocherNo = vc.ToString("0000000000");
            }
            else
            {
                IndorMedicenSaleseVocherNo = "0000000001";
            }

            ViewBag.IndorMedicenSaleseVocherNo = IndorMedicenSaleseVocherNo;
            string userid = "";
            string userSettings = "";
            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["UserName"] != null)
                { userSettings = Request.Cookies["UserSettings"]["UserName"]; userid = Request.Cookies["UserSettings"]["UserId"]; }
            }
            o.EntryDate = DateTime.Today;
            o.UserInfoID = Convert.ToInt32(userid);

            if (ModelState.IsValid)
            {
                tbl_IndorMedicineSalesVoucher fc = new tbl_IndorMedicineSalesVoucher { IndorMedicenSaleseVocherNo = IndorMedicenSaleseVocherNo, PatientID = o.PatientID, TotalAmount = o.TotalAmount, VatAmount = o.VatAmount, DiscountAmount = o.DiscountAmount, GrandTotalAmount = o.GrandTotalAmount, PayAmount = o.PayAmount, DueAmount = o.DueAmount, PayDate = o.PayDate, UserInfoID = o.UserInfoID, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate };
                foreach (var i in o.tbl_IndorMedicineSalesDetails)
                {
                    fc.tbl_IndorMedicineSalesDetails.Add(i);
                }

                db.tbl_IndorMedicineSalesVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            var jsonData = new { success = true, status = status, IndorMedicenSaleseVocherNo = IndorMedicenSaleseVocherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult IndormedicineSaleList(tbl_IndorMedicineSalesVoucher medicine)
        {
            var results = (from item in db.tbl_IndorMedicineSalesVoucher select item).ToList().OrderBy(i => i.IndorMedicenSaleseVocherNo);
            var pageIndex = medicine.Page ?? 1;
            medicine.IndormedicineSaleListResults = results.ToPagedList(pageIndex, RecordPerPages);
            return View(medicine);
            //return View(db.IndorMedicineSalesVouchers.ToList());
        }
        [HttpPost]
        public ActionResult IndormedicineSaleList(string keyword, tbl_IndorMedicineSalesVoucher medicine)
        {
            var results = (from item in db.tbl_IndorMedicineSalesVoucher where item.tbl_PatientInfo.PatientName.Contains(keyword) select item).ToList();
            var pageIndex = medicine.Page ?? 1;
            medicine.IndormedicineSaleListResults = results.ToPagedList(pageIndex, RecordPerPages);
            return View(medicine);

        }
        //............................Upadate......................
        [HttpGet]
        public ActionResult IndorMedicineSalesEdit(tbl_IndorMedicineSalesVoucher tbv, string IndorMedicenSaleseVocherNo, ReferenceIndorMedicineSalesModel model)
        {
            ViewBag.MediInfoID = new SelectList(db.tbl_MedicineInfo, "MediInfoID", "MediName");
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            ViewBag.IndorMedicenSaleseVocherNo = IndorMedicenSaleseVocherNo;
            model.indorMedicineSalesVoucherTbl = (from item in db.tbl_IndorMedicineSalesVoucher where item.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo select item).ToList();
            model.idnorMedicineSalesDetailsTbl = (from item in db.tbl_IndorMedicineSalesDetails where item.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo select item).ToList();
            return View(model);
        }
        public JsonResult IndorMedicineSalesUpdate(tbl_IndorMedicineSalesVoucher tbv, tbl_IndorMedicineSalesDetails tbvd, string IndorMedicenSaleseVocherNo, int PatientID, decimal TotalAmount, decimal VatAmount, decimal DiscountAmount, decimal GrandTotalAmount, decimal PayAmount, decimal DueAmount, int IndorMedicenSaleseId, int MediInfoID, decimal Rate, int Quantity, decimal Amount)
        {
            tbv = db.tbl_IndorMedicineSalesVoucher.FirstOrDefault(x => x.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo);
            tbv.PatientID = PatientID;
            tbv.TotalAmount = TotalAmount;
            tbv.VatAmount = VatAmount;
            tbv.DiscountAmount = DiscountAmount;
            tbv.GrandTotalAmount = GrandTotalAmount;
            tbv.PayAmount = PayAmount;
            tbv.DueAmount = DueAmount;
            db.SaveChanges();

            tbvd = db.tbl_IndorMedicineSalesDetails.FirstOrDefault(x => x.IndorMedicenSaleseId == IndorMedicenSaleseId);
            tbvd.MediInfoID = MediInfoID;
            tbvd.Rate = Rate;
            tbvd.Quantity = Quantity;
            tbvd.Amount = Amount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult IndormedicinesalesDelete(IndorMedicineSalesVoucher tbv, IndorMedicineSalesDetails tbvd, string IndorMedicenSaleseVocherNo, int IndorMedicenSaleseId, decimal GrandTotalAmount)
        //{
        //    tbvd = db.IndorMedicineSalesDetails.FirstOrDefault(x => x.IndorMedicenSaleseId == IndorMedicenSaleseId);
        //    db.IndorMedicineSalesDetails.Remove(tbvd);
        //    db.SaveChanges();

        //    tbv = db.IndorMedicineSalesVouchers.FirstOrDefault(x => x.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo);
        //    tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

        //    tbv.PayAmount = 0;
        //    tbv.DueAmount = tbv.GrandTotalAmount;
        //    db.SaveChanges();

        //    var jsonData = new { success = true, message = "Successfully Deleted." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //....................Delete.......................
        public ActionResult Delete(tbl_IndorMedicineSalesVoucher tbv, tbl_IndorMedicineSalesDetails tbvd, string IndorMedicenSaleseVocherNo)
        {
            var info = (from item in db.tbl_IndorMedicineSalesDetails where item.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_IndorMedicineSalesDetails.Remove(vp);
            }
            else
            {
                if (IndorMedicenSaleseVocherNo != null)
                {
                    tbvd = db.tbl_IndorMedicineSalesDetails.FirstOrDefault(x => x.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo);
                    db.tbl_IndorMedicineSalesDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();

            tbv = db.tbl_IndorMedicineSalesVoucher.FirstOrDefault(x => x.IndorMedicenSaleseVocherNo == IndorMedicenSaleseVocherNo);
            db.tbl_IndorMedicineSalesVoucher.Remove(tbv);
            db.SaveChanges();

            return RedirectToAction("IndormedicineSaleList");
        }
	}
}