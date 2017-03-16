using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class LeaveController : Controller
    {
        const int recordsPerPages = 3;
        public static int leaveid = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addLeave()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult addLeave(tbl_Leave leav)
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
                leav.EntryDate = DateTime.Today;
                leav.UserID = Convert.ToInt32(userid);
                db.tbl_Leave.Add(leav);
                db.SaveChanges();
                ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
                ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
                ViewBag.Messages = "Save Succesfully";
                return RedirectToAction("addLeave");
            }

            return View(leav);

        }
        public ActionResult leaveList(tbl_Leave leave)
        {
            var results = (from item in db.tbl_Leave select item).ToList().OrderBy(l => l.LeaveID);
            var pageIndex = leave.Page ?? 1;
            leave.leaveListResults = results.ToPagedList(pageIndex, recordsPerPages);
            return View(leave);
        }

        [HttpPost]
        public ActionResult leaveList(string keyword, tbl_Leave leave)
        {
            var results = (from item in db.tbl_Leave where item.tbl_staffInfo.Name.Contains(keyword) || item.LeaveCause.Contains(keyword) select item).ToList();
            var pageIndex = leave.Page ?? 1;
            leave.leaveListResults = results.ToPagedList(pageIndex, recordsPerPages);
            return View(leave);
        }

        [HttpGet]
        public ActionResult leaveUpdate(int id = 0)
        {
            leaveid = id;
            tbl_Leave bc = db.tbl_Leave.FirstOrDefault(l => l.LeaveID == leaveid);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetLeaveDateFrom = bc.LeaveDateFrom;
            ViewBag.GetLeaveDateTo = bc.LeaveDateTo;
            ViewBag.GetTotalLeaveDays = bc.TotalLeaveDays;
            ViewBag.GetLeaveCause = bc.LeaveCause;
            ViewBag.GetTotalLeave = bc.TotalLeave;
            return View(bc);
        }
        [HttpPost]
        public ActionResult leaveUpdate(tbl_Leave bc, int id = 0)
        {
            tbl_Leave bc1 = db.tbl_Leave.FirstOrDefault(l => l.LeaveID == leaveid);
            bc1.StaffID = bc.StaffID;
            bc1.LeaveDateFrom = bc.LeaveDateFrom;
            bc1.LeaveDateTo = bc.LeaveDateTo;
            bc1.TotalLeaveDays = bc.TotalLeaveDays;
            bc1.LeaveCause = bc.LeaveCause;
            bc1.TotalLeave = bc.TotalLeave;
            if (bc == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.Messages = "Update Successfully";
            return View(bc1);
        }

        public ActionResult leaveDelete(int id = 0)
        {
            tbl_Leave bc1 = db.tbl_Leave.FirstOrDefault(l => l.LeaveID == id);
            db.tbl_Leave.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("leaveList");
        }
	}
}