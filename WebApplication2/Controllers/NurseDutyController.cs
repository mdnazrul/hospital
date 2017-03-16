using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class NurseDutyController : Controller
    {
        const int RecordPerPage = 3;
        public static int nursedutyid = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addNurseDuty()
        {
            var info = (from item in db.tbl_StaffType where item.StaffName == "Nurse" select item).FirstOrDefault();
            var nurseInfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();

            ViewBag.StaffID = new SelectList(nurseInfo, "StaffID", "Name");
            ViewBag.SectionID = new SelectList(db.tbe_NurseSectionType, "SectionID", "SectionName");
            return View();
        }

        [HttpPost]
        public ActionResult addNurseDuty(tbl_NurseDuty nurseDuty)
        {
           
            if (ModelState.IsValid)
            {
                db.tbl_NurseDuty.Add(nurseDuty);
                db.SaveChanges();
                return RedirectToAction("nurseDutyList");
            }
            var info = (from item in db.tbl_StaffType where item.StaffName == "Nurse" select item).FirstOrDefault();
            var nurseInfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            ViewBag.StaffID = new SelectList(nurseInfo, "StaffID", "Name");
            ViewBag.SectionID = new SelectList(db.tbe_NurseSectionType, "SectionID", "SectionName");
            return View(nurseDuty);
        }
        public ActionResult nurseDutyList(tbl_NurseDuty nurseduty)
        {
            var results = (from item in db.tbl_NurseDuty select item).ToList().OrderBy(s => s.NurseDutyID);
            var pageIndex = nurseduty.Page ?? 1;
            nurseduty.nurseDutyListResults = results.ToPagedList(pageIndex, RecordPerPage);
            return View(nurseduty);
        }
        [HttpPost]
        public ActionResult nurseDutyList(string keyword, tbl_NurseDuty nurseduty)
        {
            var results = (from item in db.tbl_NurseDuty where item.tbl_staffInfo.Name.Contains(keyword) || item.tbe_NurseSectionType.SectionName.Contains(keyword) select item).ToList();
            var pageIndex = nurseduty.Page ?? 1;
            nurseduty.nurseDutyListResults = results.ToPagedList(pageIndex, RecordPerPage);
            return View(nurseduty);
        }
        [HttpGet]
        public ActionResult nursedutyUpdate(int id = 0)
        {
            nursedutyid = id;
            tbl_NurseDuty bc = db.tbl_NurseDuty.FirstOrDefault(s => s.NurseDutyID == nursedutyid);
            if (bc == null)
            {
                return HttpNotFound();
            }
            var info = (from item in db.tbl_StaffType where item.StaffName == "Nurse" select item).FirstOrDefault();
            var nurseInfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            ViewBag.StaffID = new SelectList(nurseInfo, "StaffID", "Name");
            ViewBag.SectionID = new SelectList(db.tbe_NurseSectionType, "SectionID", "SectionName");
            ViewBag.GetRoomNo = bc.RoomNo;
            ViewBag.GetShift = bc.Shift;
            return View(bc);
        }
        [HttpPost]
        public ActionResult nursedutyUpdate(tbl_NurseDuty bc, int id = 0)
        {
            tbl_NurseDuty bc1 = db.tbl_NurseDuty.FirstOrDefault(s => s.NurseDutyID == nursedutyid);
            bc1.StaffID = bc.StaffID;
            bc1.SectionID = bc.SectionID;
            bc1.RoomNo = bc.RoomNo;
            bc1.Shift = bc.Shift;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            var info = (from item in db.tbl_StaffType where item.StaffName == "Nurse" select item).FirstOrDefault();
            var nurseInfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            ViewBag.StaffID = new SelectList(nurseInfo, "StaffID", "Name");
            ViewBag.SectionID = new SelectList(db.tbe_NurseSectionType, "SectionID", "SectionName");
            ViewBag.Messages = "Update Successfully";
            return View(bc1);

        }

        public ActionResult nursedutyDelete(int id = 0)
        {
            tbl_NurseDuty bc1 = db.tbl_NurseDuty.FirstOrDefault(s => s.NurseDutyID == id);
            db.tbl_NurseDuty.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("nurseDutyList");
        }
	}
}