using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class BloodSupplierProfileController : Controller
    {
        const int RecordsPerPage = 3;
        public static int bloodSupplireProfileId = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addBloodSupplierProfile()
        {
            string allowedChars = "";
            allowedChars = "0,1,2,3,4,5,6,7,8,9";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string genIdString = "";
            string temp = "";

            //Random rand = new Random();
            Random rand = new Random(DateTime.Now.Millisecond);
            //rand.Next();

            for (int i = 0; i < 8; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                genIdString += temp;
            }

            ViewBag.VoucherNo = genIdString;

            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult addBloodSupplierProfile(tbl_BloodSupplierProfile bloodSupplier)
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
                bloodSupplier.EntryDate = DateTime.Today;
                bloodSupplier.UserID = Convert.ToInt32(userid);
                db.tbl_BloodSupplierProfile.Add(bloodSupplier);
                db.SaveChanges();
                return RedirectToAction("bloodSupplierProfileLists");
            }

            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(bloodSupplier);
        }

        public ActionResult bloodSupplierProfileLists(tbl_BloodSupplierProfile bloodSupplier)
        {
            var results = (from item in db.tbl_BloodSupplierProfile
                           select item).ToList().OrderBy(b => b.BloodSupplierProfileID);
            var pageIndex = bloodSupplier.Page ?? 1;
            bloodSupplier.bloodSupplierProfileListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(bloodSupplier);
            //return View(db.BloodSupplierProfiles.ToList());
        }

        [HttpPost]
        public ActionResult bloodSupplierProfileLists(string keyword, tbl_BloodSupplierProfile bloodSupplier)
        {
            var results = (from item in db.tbl_BloodSupplierProfile where item.tbl_BloodSupplierInfo.CompanyName.Contains(keyword) select item).ToList();
            var pageIndex = bloodSupplier.Page ?? 1;
            bloodSupplier.bloodSupplierProfileListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(bloodSupplier);
        }

        [HttpGet]
        public ActionResult bloodSupplierProfielUpdate(int id = 0)
        {
            bloodSupplireProfileId = id;
            tbl_BloodSupplierProfile bc = db.tbl_BloodSupplierProfile.FirstOrDefault(b => b.BloodSupplierProfileID == bloodSupplireProfileId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
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
        public ActionResult bloodSupplierProfielUpdate(tbl_BloodSupplierProfile bc, int id = 0)
        {
            tbl_BloodSupplierProfile bc1 = db.tbl_BloodSupplierProfile.FirstOrDefault(m => m.BloodSupplierProfileID == bloodSupplireProfileId);
            bc1.BloodSupplyerID = bc.BloodSupplyerID;
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
            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
            return View(bc1);
        }

        public JsonResult BloodSupplierProfileDelete(int id = 0)
        {
            tbl_BloodSupplierProfile bc1 = db.tbl_BloodSupplierProfile.Where(m => m.BloodSupplierProfileID == id).FirstOrDefault();

            db.tbl_BloodSupplierProfile.Remove(bc1);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}