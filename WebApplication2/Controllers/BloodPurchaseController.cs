using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class BloodPurchaseController : Controller
    {
        const int RecordsPerPage = 3;
        public static int bloodPurchaseId = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string BloodPurchaseVoucherNo = "";
        public ActionResult addBloodPurchase()
        {
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
            int vc;
            var MaxValue = (from item in db.tbl_BloodPurchaseVoucher select item.BloodPurchaseVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                BloodPurchaseVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                BloodPurchaseVoucherNo = "0000000001";
            }
            ViewBag.BloodPurchaseVoucherNo = BloodPurchaseVoucherNo;
            return View();
        }

        public JsonResult indorBloodPurchaseProcess(tbl_BloodPurchaseVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_BloodPurchaseVoucher select item.BloodPurchaseVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                BloodPurchaseVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                BloodPurchaseVoucherNo = "0000000001";
            }

            ViewBag.BloodPurchaseVoucherNo = BloodPurchaseVoucherNo;
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
                tbl_BloodPurchaseVoucher fc = new tbl_BloodPurchaseVoucher { BloodPurchaseVoucherNo = BloodPurchaseVoucherNo, PurchaseDate = o.PurchaseDate, BloodSupplyerID = o.BloodSupplyerID, GrandTotalAmount = o.GrandTotalAmount, PayAmount = o.PayAmount, DueAmount = o.DueAmount, Remark = o.Remark, UserInfoID = o.UserInfoID, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate };
                foreach (var i in o.tbl_BloodPurchaseeDetails)
                {
                    fc.tbl_BloodPurchaseeDetails.Add(i);
                }

                db.tbl_BloodPurchaseVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
            var jsonData = new { success = true, status = status, BloodPurchaseVoucherNo = BloodPurchaseVoucherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //..................list.......................
        public ActionResult bloodpurchaseList(tbl_BloodPurchaseVoucher blood)
        {
            var results = (from item in db.tbl_BloodPurchaseVoucher
                           select item).ToList().OrderBy(p => p.BloodPurchaseVoucherNo);
            var pageIndex = blood.Page ?? 1;
            blood.bloodpurchaseListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(blood);
            //return View(db.BloodPurchaseVouchers.ToList());
        }
        [HttpPost]
        public ActionResult bloodpurchaseList(string keyword, tbl_BloodPurchaseVoucher blood)
        {

            var results = (from item in db.tbl_BloodPurchaseVoucher where item.tbl_BloodSupplierInfo.CompanyName.Contains(keyword) select item).ToList();
            var pageIndex = blood.Page ?? 1;
            blood.bloodpurchaseListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(blood);

        }

        //............................Upadate......................
        [HttpGet]
        public ActionResult VoucherEdit(tbl_BloodPurchaseVoucher tbv, string BloodPurchaseVoucherNo, ReferenceBloodPurchaseModel model)
        {
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.BloodSupplyerID = new SelectList(db.tbl_BloodSupplierInfo, "BloodSupplyerID", "CompanyName");
            ViewBag.BloodPurchaseVoucherNo = BloodPurchaseVoucherNo;
            model.VoucherTbl = (from item in db.tbl_BloodPurchaseVoucher where item.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo select item).ToList();
            model.VoucherDetailsTbl = (from item in db.tbl_BloodPurchaseeDetails where item.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo select item).ToList();
            return View(model);
        }
        public JsonResult VoucherUpdate(tbl_BloodPurchaseVoucher tbv, tbl_BloodPurchaseeDetails tbvd, string BloodPurchaseVoucherNo, DateTime PurchaseDate, int BloodSupplyerID, decimal GrandTotalAmount, decimal PayAmount, decimal DueAmount, int BloodPurchaseID, int BloodID, decimal Rate, decimal Quantity, decimal Amount)
        {
            tbv = db.tbl_BloodPurchaseVoucher.FirstOrDefault(x => x.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo);
            tbv.PurchaseDate = PurchaseDate;
            tbv.BloodSupplyerID = BloodSupplyerID;
            tbv.GrandTotalAmount = GrandTotalAmount;
            tbv.PayAmount = PayAmount;
            tbv.DueAmount = DueAmount;
            db.SaveChanges();

            tbvd = db.tbl_BloodPurchaseeDetails.FirstOrDefault(x => x.BloodPurchaseID == BloodPurchaseID);
            tbvd.BloodID = BloodID;
            tbvd.Rate = Rate;
            tbvd.Quantity = Quantity;
            tbvd.Amount = Amount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VoucherDelete(tbl_BloodPurchaseVoucher tbv, tbl_BloodPurchaseeDetails tbvd, string BloodPurchaseVoucherNo, int BloodPurchaseID, decimal GrandTotalAmount)
        {
            tbvd = db.tbl_BloodPurchaseeDetails.FirstOrDefault(x => x.BloodPurchaseID == BloodPurchaseID);
            db.tbl_BloodPurchaseeDetails.Remove(tbvd);
            db.SaveChanges();

            tbv = db.tbl_BloodPurchaseVoucher.FirstOrDefault(x => x.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo);
            tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

            tbv.PayAmount = 0;
            tbv.DueAmount = tbv.GrandTotalAmount;
            db.SaveChanges();

            var jsonData = new { success = true, message = "Successfully Deleted." };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //......................................Delete.............................

        public ActionResult Delete(tbl_BloodPurchaseVoucher tbv, tbl_BloodPurchaseeDetails tbvd, string BloodPurchaseVoucherNo)
        {
            var info = (from item in db.tbl_BloodPurchaseeDetails where item.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var bpv in info)
                    db.tbl_BloodPurchaseeDetails.Remove(bpv);
            }
            else
            {
                if (BloodPurchaseVoucherNo != null)
                {
                    tbvd = db.tbl_BloodPurchaseeDetails.FirstOrDefault(x => x.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo);
                    db.tbl_BloodPurchaseeDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();
            tbv = db.tbl_BloodPurchaseVoucher.FirstOrDefault(x => x.BloodPurchaseVoucherNo == BloodPurchaseVoucherNo);
            db.tbl_BloodPurchaseVoucher.Remove(tbv);
            db.SaveChanges();
            return RedirectToAction("bloodpurchaseList");
        }
	}
}