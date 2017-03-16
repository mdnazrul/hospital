using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class LoanController : Controller
    {
        public static int loanid = 0;
        const int recordPerPages = 3;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addLoan()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult addLoan(tbl_Loan loan)
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
                loan.EntryDate = DateTime.Today;
                loan.UserID = Convert.ToInt32(userid);
                db.tbl_Loan.Add(loan);
                db.SaveChanges();
                return RedirectToAction("loanList");
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(loan);
        }

        public ActionResult loanList(tbl_Loan loan)
        {
            var results = (from item in db.tbl_Loan select item).ToList().OrderBy(l => l.LoanID);
            var pageIndex = loan.Page ?? 1;
            loan.loanListResults = results.ToPagedList(pageIndex, recordPerPages);
            return View(loan);
        }
        [HttpPost]
        public ActionResult loanList(string keyword, tbl_Loan loan)
        {
            var results = (from item in db.tbl_Loan where item.tbl_staffInfo.Name.Contains(keyword) || item.PaymentWay.Contains(keyword) select item).ToList();
            var pageIndex = loan.Page ?? 1;
            loan.loanListResults = results.ToPagedList(pageIndex, recordPerPages);
            return View(loan);
        }

        [HttpGet]
        public ActionResult loanUpdate(int id = 0)
        {
            loanid = id;
            tbl_Loan bc = db.tbl_Loan.FirstOrDefault(l => l.LoanID == loanid);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetSalary = bc.Salary;
            ViewBag.GetLoanAmount = bc.LoanAmount;
            ViewBag.GetNewLoan = bc.NewLoan;
            ViewBag.GetLoanBack = bc.LoanBack;
            ViewBag.GetPaymentWay = bc.PaymentWay;
            ViewBag.GetGrandTotalLoan = bc.GrandTotalLoan;
            ViewBag.GetPaidLoan = bc.PaidLoan;
            return View(bc);
        }
        [HttpPost]
        public ActionResult loanUpdate(tbl_Loan bc, int id = 0)
        {
            tbl_Loan bc1 = db.tbl_Loan.FirstOrDefault(l => l.LoanID == loanid);
            bc1.StaffID = bc.StaffID;
            bc1.Salary = bc.Salary;
            bc1.LoanAmount = bc.LoanAmount;
            bc1.NewLoan = bc.NewLoan;
            bc1.LoanBack = bc.LoanBack;
            bc1.PaymentWay = bc.PaymentWay;
            bc1.GrandTotalLoan = bc.GrandTotalLoan;
            bc1.PaidLoan = bc.PaidLoan;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.Messages = "Update Succesfully!!";
            return View(bc1);

        }
        public ActionResult loanDelete(int id = 0)
        {
            tbl_Loan bc1 = db.tbl_Loan.FirstOrDefault(l => l.LoanID == id);
            db.tbl_Loan.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("loanList");
        }
        public decimal LoanAmount = 0;
        public JsonResult GetEmpInfo(int StaffId)
        {
            var Info = (from item in db.tbl_staffInfo where item.StaffID == StaffId select item).FirstOrDefault();
            var info1 = (from item in db.tbl_Loan where item.StaffID == StaffId select item).ToList();

            var jsonData = new { success = true, basicSalary = Info.BasicSalary, loanAmount = LoanAmount };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
	}
}