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
    public class AdvancedController : Controller
    {
        const int RecordsPerPage = 3;
        public static int advanceid = 0;
        public decimal AdvanceAmount = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();

        //Hide the Index start.................................

        //public ActionResult Index(Advance model, string tResults = null)
        //{         
        //    var test = tResults;
        //    //For Search Key
        //    model.IndexResults = null;


        //    if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
        //    {
        //        if (!string.IsNullOrEmpty(model.SearchButton) && model.AdvanceID == 0)
        //        {
        //            ModelState.AddModelError("", "Course Name is Required");
        //            return View(model);
        //        }
        //        else
        //        {
        //            if (model.AdvanceID == 0)
        //            {
        //                var results = (from item in db.Advances
        //                               select item).ToList().OrderBy(p => p.AdvanceID);

        //                var pageIndex = model.Page ?? 1;
        //                model.IndexResults = results.ToPagedList(pageIndex, RecordsPerPage);

        //                if (model.IndexResults.Count() == 0)
        //                {
        //                    var results1 = (from item in db.Advances
        //                                    select item).ToList().OrderBy(p => p.AdvanceID);

        //                    pageIndex = pageIndex - 1;
        //                    model.IndexResults = results1.ToPagedList(pageIndex, RecordsPerPage);
        //                    return View(model);

        //                }
        //                else
        //                {
        //                    return View(model);
        //                }
        //            }
        //            else
        //            {
        //                var results = (from item in db.Advances
        //                               where item.AdvanceID == model.AdvanceID
        //                               select item).ToList().OrderBy(p => p.AdvanceID);

        //                var pageIndex = model.Page ?? 1;
        //                model.IndexResults = results.ToPagedList(pageIndex, RecordsPerPage);
        //                return View(model);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (test != null)
        //        {
        //            model.IndexResults = null;
        //            //var results = (from item in db.Countries
        //            //               where search.All(s => item.CountryName.Contains(s))
        //            //               select item).ToList().OrderBy(p => p.CountryId);
        //            var results = (from item in db.Advances
        //                           where item.tbl_staffInfo.Name.StartsWith(test)
        //                           select item).ToList().OrderBy(p => p.AdvanceID); ;

        //            var pageIndex = model.Page ?? 1;
        //            model.IndexResults = results.ToPagedList(pageIndex, RecordsPerPage);
        //            ViewBag.searchstring = test;
        //            return View(model);
        //        }
        //        else
        //        {
        //            var results = (from item in db.Advances
        //                           select item).ToList().OrderBy(p => p.AdvanceID);

        //            var pageIndex = model.Page ?? 1;
        //            model.IndexResults = results.ToPagedList(pageIndex, RecordsPerPage);
        //            return View(model);
        //        }
        //        //return View(model);

        //    }
        //}

        //Hide the Index End.................................


        public ActionResult addAdvanced()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult addAdvanced(tbl_Advance advance)
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
                advance.EntryDate = DateTime.Today;
                advance.UserInfoID = Convert.ToInt32(userid);

                db.tbl_Advance.Add(advance);
                db.SaveChanges();
                return RedirectToAction("addAdvanced");
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(advance);
        }

        public ActionResult AdvancedList(tbl_Advance advance)
        {
            var results = (from item in db.tbl_Advance
                           select item).ToList().OrderBy(p => p.AdvanceID);
            var pageIndex = advance.Page ?? 1;
            advance.AdvancedListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(advance);
            //return View(db.Advances.ToList());
        }

        [HttpPost]
        public ActionResult AdvancedList(string keyword, tbl_Advance advance)
        {
            var results = (from item in db.tbl_Advance where item.tbl_staffInfo.Name.Contains(keyword) select item).ToList();
            var pageIndex = advance.Page ?? 1;
            advance.AdvancedListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(advance);
        }

        [HttpGet]
        public ActionResult advanceUpdate(int id = 0)
        {
            advanceid = id;
            tbl_Advance bc = db.tbl_Advance.FirstOrDefault(a => a.AdvanceID == advanceid);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetSalary = bc.Salary;
            ViewBag.GetAdvanceDate = bc.AdvanceDate;
            ViewBag.GetAdvanceAmount = bc.AdvanceAmount;
            ViewBag.GetNewAdvance = bc.NewAdvance;
            ViewBag.GetAdvanceBack = bc.AdvanceBack;
            ViewBag.GetPaymentWay = bc.PaymentWay;
            ViewBag.GetGrandTotalAmount = bc.GrandTotalAmount;
            ViewBag.GetPaid = bc.Paid;
            return View(bc);
        }

        [HttpPost]
        public ActionResult advanceUpdate(tbl_Advance bc, int id = 0)
        {
            tbl_Advance bc1 = db.tbl_Advance.FirstOrDefault(a => a.AdvanceID == advanceid);
            bc1.StaffID = bc.StaffID;
            bc1.Salary = bc.Salary;
            bc1.AdvanceDate = bc.AdvanceDate;
            bc1.AdvanceAmount = bc.AdvanceAmount;
            bc1.NewAdvance = bc.NewAdvance;
            bc1.AdvanceBack = bc.AdvanceBack;
            bc1.PaymentWay = bc.PaymentWay;
            bc1.GrandTotalAmount = bc.GrandTotalAmount;
            bc1.Paid = bc.Paid;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View(bc1);
        }


        // GET: /Advance/Delete/5
        public JsonResult advancedDelete(int id = 0)
        {
            tbl_Advance advance = db.tbl_Advance.Where(x => x.AdvanceID == id).FirstOrDefault();
            db.tbl_Advance.Remove(advance);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmpInfo(int StaffId)
        {
            var Info = (from item in db.tbl_staffInfo where item.StaffID == StaffId select item).FirstOrDefault();
            var info2 = (from item in db.tbl_Advance where item.StaffID == StaffId select item).ToList();
            if (info2.Count > 0)
            {
                foreach (var item in info2)
                {
                    AdvanceAmount = Convert.ToDecimal(item.AdvanceAmount);
                }
            }
            else
            {
                AdvanceAmount = 0;
            }
            var jsonData = new { success = true, basicSalary = Info.BasicSalary, advanceamount = AdvanceAmount };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
	}
}