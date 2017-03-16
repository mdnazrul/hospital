using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class SalaryController : Controller
    {
        const int RecordsPerPage = 3;
        public static int salaryid = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addsalary()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult addsalary(tbl_Salary salary)
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
                salary.EnterDate = DateTime.Today;
                salary.UserInfoID = Convert.ToInt32(userid);
                db.tbl_Salary.Add(salary);
                db.SaveChanges();
                return RedirectToAction("addsalary");
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserInfoID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(salary);
        }
        public ActionResult salaryList(tbl_Salary salary)
        {
            var results = (from item in db.tbl_Salary
                           select item).ToList().OrderBy(p => p.SalaryID);
            var pageIndex = salary.Page ?? 1;
            salary.SalaryListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(salary);
            //return View(db.Salaries.ToList());
        }
        [HttpPost]
        public ActionResult salaryList(string keyword, tbl_Salary salary)
        {
            var results = (from item in db.tbl_Salary where item.tbl_staffInfo.Name.Contains(keyword) select item).ToList();
            var pageIndex = salary.Page ?? 1;
            salary.SalaryListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(salary);
        }
        [HttpGet]
        public ActionResult salaryUpdate(int id = 0)
        {
            salaryid = id;
            tbl_Salary bc = db.tbl_Salary.FirstOrDefault(s => s.SalaryID == salaryid);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetBasicAmount = bc.BasicAmount;
            ViewBag.GetAllowanceAmount = bc.AllowanceAmount;
            ViewBag.GetTotalAmount = bc.TotalAmount;
            ViewBag.GetDetuctionAmount = bc.DetuctionAmount;
            ViewBag.GetAdvanceAmount = bc.AdvanceAmount;
            ViewBag.GetAdvanceBack = bc.AdvanceBack;
            ViewBag.GetLoanAmount = bc.LoanAmount;
            ViewBag.GetLoanBack = bc.LoanBack;
            ViewBag.GetGrandTotalAmount = bc.GrandTotalAmount;
            return View(bc);
        }
        [HttpPost]
        public ActionResult salaryUpdate(tbl_Salary bc, int id = 0)
        {
            tbl_Salary bc1 = db.tbl_Salary.FirstOrDefault(s => s.SalaryID == salaryid);
            bc1.StaffID = bc.StaffID;
            bc1.BasicAmount = bc.BasicAmount;
            bc1.AllowanceAmount = bc.AllowanceAmount;
            bc1.TotalAmount = bc.TotalAmount;
            bc1.DetuctionAmount = bc.DetuctionAmount;
            bc1.AdvanceAmount = bc.AdvanceAmount;
            bc1.AdvanceBack = bc.AdvanceBack;
            bc1.LoanAmount = bc.LoanAmount;
            bc1.LoanBack = bc.LoanBack;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View(bc1);

        }
        public JsonResult SalaryDelete(int id = 0)
        {
            tbl_Salary salary = db.tbl_Salary.Where(x => x.SalaryID == id).FirstOrDefault();
            db.tbl_Salary.Remove(salary);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public decimal LoanAmount = 0;
        public decimal AdvanceAmount = 0;
        public string Designation = "";
        //public decimal BasicAmount = 0;
        public JsonResult GetEmpInfo(int StaffId)
        {
            var Info = (from item in db.tbl_staffInfo where item.StaffID == StaffId select item).FirstOrDefault();
            var info1 = (from item in db.tbl_Salary where item.StaffID == StaffId select item).ToList();
            var info2 = (from item in db.tbl_Salary where item.StaffID == StaffId select item).ToList();
            if (info1.Count > 0)
            {
                foreach (var item in info1)
                {
                    LoanAmount = Convert.ToDecimal(item.LoanAmount);
                    //LoanBack = Convert.ToDecimal(item.LoanBack);
                }
            }
            else
            {
                LoanAmount = 0;
                //LoanBack = 0;
            }
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
            var jsonData = new { success = true, basicSalary = Info.BasicSalary, designation = Info.tbl_DesignationType.DesingnationName, loanamount = LoanAmount, advanceAmount = AdvanceAmount };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
	}
}