using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class OutdorBloodSalesController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string OutdorBloodSalesVoucherNo = "";
        const int RecordsPerPage = 3;
        public static int OutdoorBloodSalesid = 0;
        public ActionResult addOutdorBloodSales()
        {
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");

            int vc;
            var MaxValue = (from item in db.tbl_OutdorBloodSalesVoucher select item.OutdorBloodSalesVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                OutdorBloodSalesVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                OutdorBloodSalesVoucherNo = "0000000001";
            }
            ViewBag.OutdorBloodSalesVoucherNo = OutdorBloodSalesVoucherNo;
            return View();
        }
        public JsonResult OutdorBloodSaleProcess(tbl_OutdorBloodSalesVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_OutdorBloodSalesVoucher select item.OutdorBloodSalesVoucherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                OutdorBloodSalesVoucherNo = vc.ToString("0000000000");
            }
            else
            {
                OutdorBloodSalesVoucherNo = "0000000001";
            }

            ViewBag.OutdorBloodSalesVoucherNo = OutdorBloodSalesVoucherNo;
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
                tbl_OutdorBloodSalesVoucher fc = new tbl_OutdorBloodSalesVoucher { OutdorBloodSalesVoucherNo = OutdorBloodSalesVoucherNo, OutdorBuyerName = o.OutdorBuyerName, OutdorBuyerPnoneNo = o.OutdorBuyerPnoneNo, OutdorBuyerAddress = o.OutdorBuyerAddress, GrandTotalAmount = o.GrandTotalAmount, PayAmount = o.PayAmount, DeuAmount = o.DeuAmount, Remarks = o.Remarks, UserInfoID = o.UserInfoID, EntryDate = o.EntryDate, UpdateDate = o.UpdateDate };
                foreach (var i in o.tbl_OutdorBloodSalesDetails)
                {
                    fc.tbl_OutdorBloodSalesDetails.Add(i);
                }

                db.tbl_OutdorBloodSalesVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            var jsonData = new { success = true, status = status, OutdorBloodSalesVoucherNo = OutdorBloodSalesVoucherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutdoorBloodSalesList(tbl_OutdorBloodSalesVoucher Oblood)
        {

            var results = (from item in db.tbl_OutdorBloodSalesVoucher
                           select item).ToList().OrderBy(p => p.OutdorBloodSalesVoucherNo);
            var pageIndex = Oblood.Page ?? 1;
            Oblood.OutdoorBloodSalesListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(Oblood);
            //return View(db.OutdorBloodSalesVouchers.ToList());
        }
        [HttpPost]
        public ActionResult OutdoorBloodSalesList(string keyword, tbl_OutdorBloodSalesVoucher Oblood)
        {

            var results = (from item in db.tbl_OutdorBloodSalesVoucher where item.OutdorBuyerName.Contains(keyword) || item.OutdorBuyerPnoneNo.Contains(keyword) select item).ToList();
            var pageIndex = Oblood.Page ?? 1;
            Oblood.OutdoorBloodSalesListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(Oblood);

        }

        //............................Upadate......................
        [HttpGet]
        public ActionResult OutDoorbloodSalesEdit(tbl_OutdorBloodSalesVoucher tbv, string OutdorBloodSalesVoucherNo, ReferenceOutdoorBloodSalesModel model)
        {
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.OutdorBloodSalesVoucherNo = OutdorBloodSalesVoucherNo;
            model.OutDoorBloodSalesVoucherTbl = (from item in db.tbl_OutdorBloodSalesVoucher where item.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo select item).ToList();
            model.OutdoorbloodSalesDeltailsTbl = (from item in db.tbl_OutdorBloodSalesDetails where item.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo select item).ToList();
            return View(model);
        }
        public JsonResult OutDoorBloodSalesUpdate(tbl_OutdorBloodSalesVoucher tbv, tbl_OutdorBloodSalesDetails tbvd, string OutdorBloodSalesVoucherNo, string OutdorBuyerName, string OutdorBuyerPnoneNo, decimal GrandTotalAmount, decimal PayAmount, decimal DeuAmount, int OutdorBloodSalesID, int BloodID, int Quantity, decimal Rate, int MediUnitID, decimal TotalAmount)
        {
            tbv = db.tbl_OutdorBloodSalesVoucher.FirstOrDefault(x => x.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo);
            tbv.OutdorBuyerName = OutdorBuyerName;
            tbv.OutdorBuyerPnoneNo = OutdorBuyerPnoneNo;
            tbv.GrandTotalAmount = GrandTotalAmount;
            tbv.PayAmount = PayAmount;
            tbv.DeuAmount = DeuAmount;
            db.SaveChanges();

            tbvd = db.tbl_OutdorBloodSalesDetails.FirstOrDefault(x => x.OutdorBloodSalesID == OutdorBloodSalesID);
            tbvd.BloodID = BloodID;
            tbvd.Quantity = Quantity;
            tbvd.Rate = Rate;
            tbvd.MediUnitID = MediUnitID;
            tbvd.TotalAmount = TotalAmount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OutdoorBloodSalesDelete(tbl_OutdorBloodSalesVoucher tbv, tbl_OutdorBloodSalesDetails tbvd, string OutdorBloodSalesVoucherNo, int OutdorBloodSalesID, decimal GrandTotalAmount)
        {
            tbvd = db.tbl_OutdorBloodSalesDetails.FirstOrDefault(x => x.OutdorBloodSalesID == OutdorBloodSalesID);
            db.tbl_OutdorBloodSalesDetails.Remove(tbvd);
            db.SaveChanges();

            tbv = db.tbl_OutdorBloodSalesVoucher.FirstOrDefault(x => x.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo);
            tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

            tbv.PayAmount = 0;
            tbv.DeuAmount = tbv.GrandTotalAmount;
            db.SaveChanges();

            var jsonData = new { success = true, message = "Successfully Deleted." };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        //................Delete....................

        //public JsonResult OBSalesDelete(OutdorBloodSalesVoucher tbv, OutdorBloodSalesDetails tbvd, string OutdorBloodSalesVoucherNo, int OutdorBloodSalesID)
        //{
        //    tbvd = db.OutdorBloodSalesDetails.FirstOrDefault(x => x.OutdorBloodSalesID == OutdorBloodSalesID);
        //    db.OutdorBloodSalesDetails.Remove(tbvd);
        //    db.SaveChanges();

        //    tbv = db.OutdorBloodSalesVouchers.FirstOrDefault(x => x.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo);
        //    db.OutdorBloodSalesVouchers.Remove(tbv);
        //    db.SaveChanges();

        //    var jsonData = new { success = true, message = "Successfully Deleted." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}  

        //........................Delete.............
        public ActionResult Delete(tbl_OutdorBloodSalesVoucher tbv, tbl_OutdorBloodSalesDetails tbvd, string OutdorBloodSalesVoucherNo)
        {
            var info = (from item in db.tbl_OutdorBloodSalesDetails where item.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_OutdorBloodSalesDetails.Remove(vp);
            }
            else
            {
                if (OutdorBloodSalesVoucherNo != null)
                {
                    tbvd = db.tbl_OutdorBloodSalesDetails.FirstOrDefault(x => x.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo);
                    db.tbl_OutdorBloodSalesDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();

            tbv = db.tbl_OutdorBloodSalesVoucher.FirstOrDefault(x => x.OutdorBloodSalesVoucherNo == OutdorBloodSalesVoucherNo);
            db.tbl_OutdorBloodSalesVoucher.Remove(tbv);
            db.SaveChanges();

            return RedirectToAction("OutdoorBloodSalesList");
        }
	}
}