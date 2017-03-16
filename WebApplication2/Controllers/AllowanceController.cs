using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace HospitalManagementSystem.Controllers
{
    public class AllowanceController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        string AllowanceVocherNo = "";
        public static int allowanceid = 0;
        const int RecordPerPage = 3;
        public ActionResult addAllowance()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.AllowanceID = new SelectList(db.tbl_AllowanceType, "AllowanceID", "AllowanceName");

            int vc;
            var MaxValue = (from item in db.tbl_AllowanceVoucher select item.AllowanceVocherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                AllowanceVocherNo = vc.ToString("0000000000");
            }
            else
            {
                AllowanceVocherNo = "0000000001";
            }
            ViewBag.AllowanceVocherNo = AllowanceVocherNo;
            return View();
        }

        public JsonResult addAllowanceProcess(tbl_AllowanceVoucher o)
        {
            bool status = false;

            int vc;
            var MaxValue = (from item in db.tbl_AllowanceVoucher select item.AllowanceVocherNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                AllowanceVocherNo = vc.ToString("0000000000");
            }
            else
            {
                AllowanceVocherNo = "0000000001";
            }

            ViewBag.AllowanceVocherNo = AllowanceVocherNo;

            if (ModelState.IsValid)
            {
                tbl_AllowanceVoucher fc = new tbl_AllowanceVoucher { AllowanceVocherNo = AllowanceVocherNo, StaffID = o.StaffID, GrandTotalAmount = o.GrandTotalAmount };
                foreach (var i in o.tbl_AllowanceDetails)
                {
                    fc.tbl_AllowanceDetails.Add(i);
                }

                db.tbl_AllowanceVoucher.Add(fc);

                status = true;
            }
            else
            {
                status = false;
            }

            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            var jsonData = new { success = true, status = status, AllowanceVocherNo = AllowanceVocherNo };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllowanceList(tbl_AllowanceVoucher allowance)
        {
            var results = (from item in db.tbl_AllowanceVoucher select item).ToList().OrderBy(p => p.AllowanceVocherNo);
            var pageIndex = allowance.Page ?? 1;
            allowance.AllowanceListResults = results.ToPagedList(pageIndex, RecordPerPage);
            //return View(db.AllowanceVouchers.ToList());
            return View(allowance);
        }
        [HttpPost]
        public ActionResult AllowanceList(string keyword, tbl_AllowanceVoucher allowance)
        {

            var results = (from item in db.tbl_AllowanceVoucher where item.tbl_staffInfo.Name.Contains(keyword) select item).ToList();
            var pageIndex = allowance.Page ?? 1;
            allowance.AllowanceListResults = results.ToPagedList(pageIndex, RecordPerPage);
            return View(allowance);

        }

        [HttpGet]
        public ActionResult AllowancEdit(tbl_AllowanceVoucher tbv, string AllowanceVocherNo, ReferenceAllowanceModel model)
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.AllowanceID = new SelectList(db.tbl_AllowanceType, "AllowanceID", "AllowanceName");
            ViewBag.AllowanceVocherNo = AllowanceVocherNo;
            model.AllowanceVoucherTbl = (from item in db.tbl_AllowanceVoucher where item.AllowanceVocherNo == AllowanceVocherNo select item).ToList();
            model.AllowanceDeltailsTbl = (from item in db.tbl_AllowanceDetails where item.AllowanceVocherNo == AllowanceVocherNo select item).ToList();
            return View(model);
        }
        public JsonResult AllowanceUpdate(tbl_AllowanceVoucher tbv, tbl_AllowanceDetails tbvd, string AllowanceVocherNo, int StaffID, decimal GrandTotalAmount, int AllowanceDetailId, int AllowanceID, decimal AllowanceAmount)
        {
            tbv = db.tbl_AllowanceVoucher.FirstOrDefault(x => x.AllowanceVocherNo == AllowanceVocherNo);

            tbv.StaffID = StaffID;
            tbv.GrandTotalAmount = GrandTotalAmount;
            //tbv.PayAmount = PayAmount;
            //tbv.DueAmount = DueAmount;
            db.SaveChanges();

            tbvd = db.tbl_AllowanceDetails.FirstOrDefault(x => x.AllowanceDetailId == AllowanceDetailId);
            tbvd.AllowanceID = AllowanceID;
            tbvd.AllowanceAmount = AllowanceAmount;
            db.SaveChanges();
            var jsonData = new { success = true, message = "Successfully Updated" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult AllowanceDelete(AllowanceVoucher tbv, AllowanceDetails tbvd, string AllowanceVocherNo, int AllowanceDetailId, decimal GrandTotalAmount)
        //{
        //    tbvd = db.AllowanceDetails.FirstOrDefault(x => x.AllowanceDetailId == AllowanceDetailId);
        //    db.AllowanceDetails.Remove(tbvd);
        //    db.SaveChanges();

        //    tbv = db.AllowanceVouchers.FirstOrDefault(x => x.AllowanceVocherNo == AllowanceVocherNo);
        //    tbv.GrandTotalAmount = tbv.GrandTotalAmount - GrandTotalAmount;

        //    //tbv.PayAmount = 0;
        //    //tbv.DueAmount = tbv.GrandTotalAmount;
        //    db.SaveChanges();

        //    var jsonData = new { success = true, message = "Successfully Deleted." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //................Delet......................
        public ActionResult Delete(tbl_AllowanceVoucher tbv, tbl_AllowanceDetails tbvd, string AllowanceVocherNo)
        {
            var info = (from item in db.tbl_AllowanceDetails where item.AllowanceVocherNo == AllowanceVocherNo select item).ToList();
            if (info.Count > 1)
            {
                foreach (var vp in info)
                    db.tbl_AllowanceDetails.Remove(vp);
            }
            else
            {
                if (AllowanceVocherNo != null)
                {
                    tbvd = db.tbl_AllowanceDetails.FirstOrDefault(x => x.AllowanceVocherNo == AllowanceVocherNo);
                    db.tbl_AllowanceDetails.Remove(tbvd);
                }
            }
            db.SaveChanges();

            tbv = db.tbl_AllowanceVoucher.FirstOrDefault(x => x.AllowanceVocherNo == AllowanceVocherNo);
            db.tbl_AllowanceVoucher.Remove(tbv);
            db.SaveChanges();

            return RedirectToAction("AllowanceList");
        }
    }
}