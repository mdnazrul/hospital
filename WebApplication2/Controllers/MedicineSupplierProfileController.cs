using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace WebApplication2.Controllers
{
    public class MedicineSupplierProfileController : Controller
    {
        const int RecordsPerPage = 3;
        public static int medicineSupplierProfileId = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addMedicineSuplierProfile()
        {
            string allowedChars = "";
            allowedChars = "0,1,2,3,4,5,6,7,8,9";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string genIdString = "";
            string temp = "";

            Random rand = new Random(DateTime.Now.Millisecond);
            //Random rand = new Random();

            for (int i = 0; i < 8; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                genIdString += temp;
            }

            ViewBag.VoucherNo = genIdString;

            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();

        }
        [HttpPost]
        public ActionResult addMedicineSuplierProfile(tbl_MedicineSupplierProfile mSupplierProfile)
        {
            if (ModelState.IsValid)
            {

                string userid = "";
                string userSettings = "";
                if (Request.Cookies["UserSettings"] != null)
                {
                    if (Request.Cookies["UserSettings"]["UserName"] != null)
                    { userSettings = Request.Cookies["UserSettings"]["UserName"]; userid = Request.Cookies["UserSettings"]["UserId"]; }
                }
                mSupplierProfile.EntryDate = DateTime.Today;
                mSupplierProfile.UserID = Convert.ToInt32(userid);
                db.tbl_MedicineSupplierProfile.Add(mSupplierProfile);
                db.SaveChanges();
                return RedirectToAction("medicineSupplierList");
            }
            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(mSupplierProfile);
        }

        public ActionResult medicineSupplierList(tbl_MedicineSupplierProfile medicineSupplier)
        {
            var results = (from item in db.tbl_MedicineSupplierProfile
                           select item).ToList().OrderBy(m => m.SupplierProfileID);
            var pageIndex = medicineSupplier.Page ?? 1;
            medicineSupplier.MedicinesupplierProfileListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(medicineSupplier);

            //return View(db.MedicineSupplierProfiles.ToList());
        }

        [HttpPost]
        public ActionResult medicineSupplierList(string keyword, tbl_MedicineSupplierProfile medicineSupplier)
        {
            var results = (from item in db.tbl_MedicineSupplierProfile where item.tbl_MedicineSupplierInfo.MediSupplyerName.Contains(keyword) select item).ToList();
            var pageIndex = medicineSupplier.Page ?? 1;
            medicineSupplier.MedicinesupplierProfileListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(medicineSupplier);
        }
        [HttpGet]
        public ActionResult medicineSupplierProfileUpdate(int id = 0)
        {
            medicineSupplierProfileId = id;
            tbl_MedicineSupplierProfile bc = db.tbl_MedicineSupplierProfile.FirstOrDefault(m => m.SupplierProfileID == medicineSupplierProfileId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            ViewBag.GetVoucherNo = bc.VoucherNo;
            ViewBag.GetTotalAmount = bc.TotalAmount;
            ViewBag.GetPayAmount = bc.PayAmount;
            ViewBag.GetDueAmount = bc.DueAmount;
            ViewBag.GetDueAmountBack = bc.DueAmountBack;
            ViewBag.GetDueAmountBackDate = bc.DueAmountBackDate;
            ViewBag.GetGrandTotalAmount = bc.GrandTotalAmount;
            return View(bc);
        }
        [HttpPost]
        public ActionResult medicineSupplierProfileUpdate(tbl_MedicineSupplierProfile bc, int id = 0)
        {
            tbl_MedicineSupplierProfile bc1 = db.tbl_MedicineSupplierProfile.FirstOrDefault(m => m.SupplierProfileID == medicineSupplierProfileId);
            bc1.MediSupplyerID = bc.MediSupplyerID;
            bc1.VoucherNo = bc.VoucherNo;
            bc1.TotalAmount = bc.TotalAmount;
            bc1.PayAmount = bc.PayAmount;
            bc1.DueAmount = bc.DueAmount;
            bc1.DueAmountBack = bc.DueAmountBack;
            bc1.DueAmountBackDate = bc.DueAmountBackDate;
            bc1.GrandTotalAmount = bc.GrandTotalAmount;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.MediSupplyerID = new SelectList(db.tbl_MedicineSupplierInfo, "MediSupplyerID", "MediSupplyerName");
            return View(bc1);
        }


        public JsonResult MedicinesupplierProfileDelete(int id = 0)
        {
            tbl_MedicineSupplierProfile advance = db.tbl_MedicineSupplierProfile.Where(m => m.SupplierProfileID == id).FirstOrDefault();

            db.tbl_MedicineSupplierProfile.Remove(advance);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}