using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class AttendancController : Controller
    {
        const int RecordsPerPage = 3;
        public static int attendancid = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addAttendance()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult addAttendance(tbl_Attendance attend)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Attendance.Add(attend);
                db.SaveChanges();
                return RedirectToAction("attendanceList");
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(attend);
        }
        public ActionResult attendanceList(tbl_Attendance attendances)
        {
            var results = (from item in db.tbl_Attendance
                           select item).ToList().OrderBy(p => p.AttendID);
            var pageIndex = attendances.Page ?? 1;
            attendances.AttendanceListResult = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(attendances);
            //return View(db.Attendances.ToList());
        }

        [HttpPost]
        public ActionResult attendanceList(string keyword, tbl_Attendance attendances)
        {
            var results = (from item in db.tbl_Attendance where item.tbl_staffInfo.Name.Contains(keyword) || item.Sift.Contains(keyword) select item).ToList();
            var pageIndex = attendances.Page ?? 1;
            attendances.AttendanceListResult = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(attendances);
        }

        [HttpGet]
        public ActionResult AttendanceUpdate(int id = 0)
        {
            attendancid = id;
            tbl_Attendance bc = db.tbl_Attendance.FirstOrDefault(a => a.AttendID == attendancid);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetEmployeeAttendance = bc.EmployeeAttendance;
            ViewBag.GetSift = bc.Sift;
            ViewBag.GetOverTime = bc.OverTime;
            return View(bc);
        }
        [HttpPost]
        public ActionResult AttendanceUpdate(tbl_Attendance bc, int id = 0)
        {
            tbl_Attendance bc1 = db.tbl_Attendance.FirstOrDefault(a => a.AttendID == attendancid);
            bc1.StaffID = bc.StaffID;
            bc1.EmployeeAttendance = bc.EmployeeAttendance;
            bc1.Sift = bc.Sift;
            bc1.OverTime = bc.OverTime;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View(bc1);
        }
        public JsonResult attendanceDelete(int id = 0)
        {
            tbl_Attendance attendance = db.tbl_Attendance.Where(x => x.AttendID == id).FirstOrDefault();

            db.tbl_Attendance.Remove(attendance);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}