using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class OutDoorMedicineSalesController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string OutdorMedicineSalesVoucherNo = "";
        public static int OutdoorMedicinesalesid = 0;
        const int RecordPerPages = 3;
        public ActionResult addOutdoorMedicinesales()
        {
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.MediInfoID = new SelectList(db.tbl_MedicineInfo, "MediInfoID", "MediName");
            int vc;
            var MaxValue = (from item in db.tbl_OutdorMedicineSalesVoucher select item.OutdorMedicineSalesVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                OutdorMedicineSalesVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                OutdorMedicineSalesVoucherNo = "0000000001";
            }
            ViewBag.OutdorMedicineSalesVoucherNo = OutdorMedicineSalesVoucherNo;
            return View();
        }

        public JsonResult OutdoorMedicineSalesProcess(tbl_OutdorMedicineSalesVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_OutdorMedicineSalesVoucher select item.OutdorMedicineSalesVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                OutdorMedicineSalesVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                OutdorMedicineSalesVoucherNo = "0000000001";
            }

            ViewBag.OutdorMedicineSalesVoucherNo = OutdorMedicineSalesVoucherNo;
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
                tbl_OutdorMedicineSalesVoucher fc = new tbl_OutdorMedicineSalesVoucher { OutdorMedicineSalesVoucherNo = OutdorMedicineSalesVoucherNo, OutdorPatientName = o.OutdorPatientName, InvoiceNo = o.InvoiceNo, TotalAmount = o.TotalAmount, VatAmount = o.VatAmount, DiscountAmount = o.DiscountAmount, GrandTotalAmount = o.GrandTotalAmount, PayAmount = o.PayAmount, DueAmount = o.DueAmount, PayDate = o.PayDate, UserInfoID = o.UserInfoID, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate, Address = o.Address, MobNo = o.MobNo };
                foreach (var i in o.tbl_OutdorMedicineSalesDetails)
                {
                    fc.tbl_OutdorMedicineSalesDetails.Add(i);
                }

                db.tbl_OutdorMedicineSalesVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            var jsonData = new { success = true, status = status, OutdorMedicineSalesVoucherNo = OutdorMedicineSalesVoucherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult outdorMedicineSalesList(tbl_OutdorMedicineSalesVoucher medicine)
        {
            var results = (from item in db.tbl_OutdorMedicineSalesVoucher select item).ToList().OrderBy(i => i.OutdorMedicineSalesVoucherNo);
            var pageIndex = medicine.Page ?? 1;
            medicine.outdorMedicineSalesListResults = results.ToPagedList(pageIndex, RecordPerPages);
            return View(medicine);
            //return View(db.OutdorMedicineSalesVouchers.ToList());
        }
        [HttpPost]
        public ActionResult outdorMedicineSalesList(string keyword, tbl_OutdorMedicineSalesVoucher medicine)
        {
            var results = (from item in db.tbl_OutdorMedicineSalesVoucher where item.OutdorPatientName.Contains(keyword) select item).ToList();
            var pageIndex = medicine.Page ?? 1;
            medicine.outdorMedicineSalesListResults = results.ToPagedList(pageIndex, RecordPerPages);
            return View(medicine);

        }

        //............................Upadate......................
        [HttpGet]
        public ActionResult OutDoorMedicineSalesEdit(tbl_OutdorMedicineSalesVoucher tbv, string OutdorMedicineSalesVoucherNo, ReferenceOutDoorMedicineSalesModel model)
        {
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.MediInfoID = new SelectList(db.tbl_MedicineInfo, "MediInfoID", "MediName");
            ViewBag.OutdorMedicineSalesVoucherNo = OutdorMedicineSalesVoucherNo;
            model.OutdorMedicineSalesVoucherTbl = (from item in db.tbl_OutdorMedicineSalesVoucher where item.OutdorMedicineSalesVoucherNo == OutdorMedicineSalesVoucherNo select item).ToList();
            model.OutdoorMedicinesalesDeltailsTbl = (from item in db.tbl_OutdorMedicineSalesDetails where item.OutdorMedicineSalesVoucherNo == OutdorMedicineSalesVoucherNo select item).ToList();
            return View(model);
        }

        public JsonResult OutDoorMedicineSalesUpdate(tbl_OutdorMedicineSalesVoucher tbv, tbl_OutdorMedicineSalesDetails tbvd, string OutdorMedicineSalesVoucherNo, string OutdorPatientName, decimal TotalAmount, decimal VatAmount, decimal DiscountAmount, decimal GrandTotalAmount, decimal PayAmount, decimal DueAmount, int OutdorMedicenSaleseId, int MediInfoID, decimal Rate, int Quantity, decimal Amount)
        {
            tbv = db.tbl_OutdorMedicineSalesVoucher.FirstOrDefault(x => x.OutdorMedicineSalesVoucherNo == OutdorMedicineSalesVoucherNo);
            tbv.OutdorPatientName = OutdorPatientName;
            tbv.TotalAmount = TotalAmount;
            tbv.VatAmount = VatAmount;
            tbv.DiscountAmount = DiscountAmount;
            tbv.GrandTotalAmount = GrandTotalAmount;
            tbv.PayAmount = PayAmount;
            tbv.DueAmount = DueAmount;
            db.SaveChanges();

            tbvd = db.tbl_OutdorMedicineSalesDetails.FirstOrDefault(x => x.OutdorMedicenSaleseId == OutdorMedicenSaleseId);
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

        //..................Delete................
        public ActionResult Delete(tbl_OutdorMedicineSalesVoucher tbv, tbl_OutdorMedicineSalesDetails tbvd, string OutdorMedicineSalesVoucherNo)
        {
            var info = (from item in db.tbl_OutdorMedicineSalesDetails where item.OutdorMedicineSalesVoucherNo == OutdorMedicineSalesVoucherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_OutdorMedicineSalesDetails.Remove(vp);
            }
            else
            {
                if (OutdorMedicineSalesVoucherNo != null)
                {
                    tbvd = db.tbl_OutdorMedicineSalesDetails.FirstOrDefault(x => x.OutdorMedicineSalesVoucherNo == OutdorMedicineSalesVoucherNo);
                    db.tbl_OutdorMedicineSalesDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();

            tbv = db.tbl_OutdorMedicineSalesVoucher.FirstOrDefault(x => x.OutdorMedicineSalesVoucherNo == OutdorMedicineSalesVoucherNo);
            db.tbl_OutdorMedicineSalesVoucher.Remove(tbv);
            db.SaveChanges();

            return RedirectToAction("outdorMedicineSalesList");
        }
	}
}