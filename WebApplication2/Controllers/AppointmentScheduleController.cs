using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentScheduleController : Controller
    {
        const int RecordsPerPage = 3;
        public static int appointScheduleId = 0;
        string SerialNo = "";
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addAppointmentSchedule()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            int vc;
            var MaxValue = (from item in db.tbl_AppointmentSchedule select item.SerialNo);

            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                SerialNo = vc.ToString("000000000");
            }
            else
            {
                SerialNo = "000000001";
            }

            ViewBag.SerialNo = SerialNo;
            return View();

        }

        [HttpPost]
        public ActionResult addAppointmentSchedule(tbl_AppointmentSchedule appointSchedule)
        {
            int vc;
            var MaxValue = (from item in db.tbl_AppointmentSchedule select item.SerialNo);
            if (MaxValue.Count() > 0)
            {
                vc = Convert.ToInt32(MaxValue.Max()) + 1;
                SerialNo = vc.ToString("000000000");
            }
            else
            {
                SerialNo = "000000001";
            }

            ViewBag.SerialNo = SerialNo;
            if (ModelState.IsValid)
            {
                string userid = "";
                string userSettings = "";
                if (Request.Cookies["UserSettings"] != null)
                {
                    if (Request.Cookies["UserSettings"]["UserName"] != null)
                    { userSettings = Request.Cookies["UserSettings"]["UserName"]; userid = Request.Cookies["UserSettings"]["UserId"]; }
                }
                appointSchedule.EntryDate = DateTime.Today;
                appointSchedule.UserID = Convert.ToInt32(userid);

                db.tbl_AppointmentSchedule.Add(appointSchedule);
                db.SaveChanges();
                return RedirectToAction("appointmentScheduleList");
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(appointSchedule);
        }
        public ActionResult appointmentScheduleList(tbl_AppointmentSchedule model)
        {
            var results = (from item in db.tbl_AppointmentSchedule
                           select item).ToList().OrderBy(p => p.AppSchedulID);
            var pageIndex = model.Page ?? 1;
            model.appointmentScheduleListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult appointmentScheduleList(string keyword, tbl_AppointmentSchedule model)
        {
            var results = (from item in db.tbl_AppointmentSchedule where item.PatientName.Contains(keyword) || item.tbl_staffInfo.Name.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.appointmentScheduleListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }


        [HttpGet]
        public ActionResult appointmentScheduleUpdate(int id = 0)
        {
            appointScheduleId = id;
            tbl_AppointmentSchedule bc = db.tbl_AppointmentSchedule.FirstOrDefault(s => s.AppSchedulID == appointScheduleId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetPatientName = bc.PatientName;
            ViewBag.GetMobNo = bc.MobNo;
            ViewBag.GetRoomNo = bc.RoomNo;
            ViewBag.GetSerialNo = bc.SerialNo;
            return View(bc);

        }
        [HttpPost]
        public ActionResult appointmentScheduleUpdate(tbl_AppointmentSchedule bc, int id = 0)
        {
            tbl_AppointmentSchedule bc1 = db.tbl_AppointmentSchedule.FirstOrDefault(s => s.AppSchedulID == appointScheduleId);
            bc1.StaffID = bc.StaffID;
            bc1.PatientName = bc.PatientName;
            bc1.MobNo = bc.MobNo;
            bc1.RoomNo = bc.RoomNo;
            bc1.SerialNo = bc.SerialNo;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.Message = "Update Successfuly";
            return View(bc1);
        }
        public ActionResult appointmentScheduleDelete(int id = 0)
        {
            tbl_AppointmentSchedule bc1 = db.tbl_AppointmentSchedule.FirstOrDefault(s => s.AppSchedulID == id);
            db.tbl_AppointmentSchedule.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("appointmentScheduleList");
        }

    }
}