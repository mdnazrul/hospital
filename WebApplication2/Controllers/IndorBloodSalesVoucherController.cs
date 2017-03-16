using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class IndorBloodSalesVoucherController : Controller
    {
        const int RecordsPerpage = 3;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string IndorBloodsalesVoucherNo = "";

        public static string indorbloodSalesId = " ";
        public ActionResult addindorbloodsalesvoucher()
        {
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");

            int vc;
            var MaxValue = (from item in db.tbl_IndorBloodSalesVoucher select item.IndorBloodsalesVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                IndorBloodsalesVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                IndorBloodsalesVoucherNo = "0000000001";
            }
            ViewBag.IndorBloodsalesVoucherNo = IndorBloodsalesVoucherNo;
            return View();
        }
        public JsonResult indorbloodsalesvoucherProcess(tbl_IndorBloodSalesVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_IndorBloodSalesVoucher select item.IndorBloodsalesVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                IndorBloodsalesVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                IndorBloodsalesVoucherNo = "0000000001";
            }

            ViewBag.IndorBloodsalesVoucherNo = IndorBloodsalesVoucherNo;
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
                tbl_IndorBloodSalesVoucher fc = new tbl_IndorBloodSalesVoucher { IndorBloodsalesVoucherNo = IndorBloodsalesVoucherNo, BuyerName = o.BuyerName, BuyerAddress = o.BuyerAddress, GrandTotalAmount = o.GrandTotalAmount, PayAmount = o.PayAmount, DeuAmount = o.DeuAmount, Remarks = o.Remarks, UserInfoID = o.UserInfoID, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate };
                foreach (var i in o.tbl_IndorBloodSalesDetails)
                {
                    fc.tbl_IndorBloodSalesDetails.Add(i);
                }

                db.tbl_IndorBloodSalesVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            var jsonData = new { success = true, status = status, IndorBloodsalesVoucherNo = IndorBloodsalesVoucherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult indorBloodsalesLists(tbl_IndorBloodSalesVoucher indorbloodsales)
        {
            var results = (from item in db.tbl_IndorBloodSalesVoucher select item).ToList().OrderBy(i => i.IndorBloodsalesVoucherNo);
            var PageIndex = indorbloodsales.Page ?? 1;
            indorbloodsales.indorBloodsalesListsResults = results.ToPagedList(PageIndex, RecordsPerpage);
            return View(indorbloodsales);
            //return View(db.IndorBloodSalesVouchers.ToList());
        }

        [HttpPost]
        public ActionResult indorBloodsalesLists(string keyword, tbl_IndorBloodSalesVoucher indorbloodsales)
        {
            var results = (from item in db.tbl_IndorBloodSalesVoucher where item.BuyerName.Contains(keyword) select item).ToList();
            var pageIndex = indorbloodsales.Page ?? 1;
            indorbloodsales.indorBloodsalesListsResults = results.ToPagedList(pageIndex, RecordsPerpage);
            return View(indorbloodsales);
        }

        //............................Upadate......................
        [HttpGet]
        public ActionResult IndoorBloodSalesEdit(tbl_IndorBloodSalesVoucher tbv, string IndorBloodsalesVoucherNo, ReferenceIndorBloodSales model)
        {
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.IndorBloodsalesVoucherNo = IndorBloodsalesVoucherNo;
            model.indorBloodSalesVocherTbl = (from item in db.tbl_IndorBloodSalesVoucher where item.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo select item).ToList();
            model.indorBloodSalesDetailsTbl = (from item in db.tbl_IndorBloodSalesDetails where item.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo select item).ToList();
            return View(model);
        }
        public JsonResult IndoorBloodsalesUpdate(tbl_IndorBloodSalesVoucher tbv, tbl_IndorBloodSalesDetails tbvd, string IndorBloodsalesVoucherNo, string BuyerName, decimal GrandTotalAmount, decimal PayAmount, decimal DeuAmount, int IndorBloodSalesID, int BloodID, int Quantity, decimal Rate, int MediUnitID, decimal TotalAmount)
        {
            tbv = db.tbl_IndorBloodSalesVoucher.FirstOrDefault(x => x.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo);
            tbv.BuyerName = BuyerName;
            tbv.GrandTotalAmount = GrandTotalAmount;
            tbv.PayAmount = PayAmount;
            tbv.DeuAmount = DeuAmount;
            db.SaveChanges();

            tbvd = db.tbl_IndorBloodSalesDetails.FirstOrDefault(x => x.IndorBloodSalesID == IndorBloodSalesID);
            tbvd.BloodID = BloodID;
            tbvd.Quantity = Quantity;
            tbvd.Rate = Rate;
            tbvd.MediUnitID = MediUnitID;
            tbvd.TotalAmount = TotalAmount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IBloodSalesDelete(tbl_IndorBloodSalesVoucher tbv, tbl_IndorBloodSalesDetails tbvd, string IndorBloodsalesVoucherNo, int IndorBloodSalesID, decimal GrandTotalAmount)
        {
            tbvd = db.tbl_IndorBloodSalesDetails.FirstOrDefault(x => x.IndorBloodSalesID == IndorBloodSalesID);
            db.tbl_IndorBloodSalesDetails.Remove(tbvd);
            db.SaveChanges();

            tbv = db.tbl_IndorBloodSalesVoucher.FirstOrDefault(x => x.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo);
            tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

            tbv.PayAmount = 0;
            tbv.DeuAmount = tbv.GrandTotalAmount;
            db.SaveChanges();

            var jsonData = new { success = true, message = "Successfully Deleted." };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }       
        public ActionResult Delete(tbl_IndorBloodSalesVoucher tbv, tbl_IndorBloodSalesDetails tbvd, string IndorBloodsalesVoucherNo)
        {
            var info = (from item in db.tbl_IndorBloodSalesDetails where item.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_IndorBloodSalesDetails.Remove(vp);
            }
            else
            {
                if (IndorBloodsalesVoucherNo != null)
                {
                    tbvd = db.tbl_IndorBloodSalesDetails.FirstOrDefault(x => x.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo);
                    db.tbl_IndorBloodSalesDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();
            tbv = db.tbl_IndorBloodSalesVoucher.FirstOrDefault(x => x.IndorBloodsalesVoucherNo == IndorBloodsalesVoucherNo);
            db.tbl_IndorBloodSalesVoucher.Remove(tbv);
            db.SaveChanges();
            return RedirectToAction("indorBloodsalesLists");
        }
    
	}
}