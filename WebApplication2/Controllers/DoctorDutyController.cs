using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class DoctorDutyController : Controller
    {
        const int RecordPerPages = 3;
        public static int doctordutyId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addDoctorDuty()
        {

            var info = (from item in db.tbl_StaffType where item.StaffName == "Doctor" select item).FirstOrDefault();
            var docinfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            ViewBag.StaffID = new SelectList(docinfo, "StaffID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult addDoctorDuty(tbl_DoctorDuty doctorDuty)
        {
          
            if (ModelState.IsValid)
            {
                db.tbl_DoctorDuty.Add(doctorDuty);
                db.SaveChanges();
                return RedirectToAction("doctorDutyList");
            }
            var info = (from item in db.tbl_StaffType where item.StaffName == "Doctor" select item).FirstOrDefault();
            var docinfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            return View(doctorDuty);
        }
        public ActionResult doctorDutyList(tbl_DoctorDuty model)
        {
            var results = (from item in db.tbl_DoctorDuty select item).ToList().OrderBy(s => s.DoctorDutyID);
            var PageIndex = model.Page ?? 1;
            model.doctorDutyListResults = results.ToPagedList(PageIndex, RecordPerPages);
            return View(model);
        }
        [HttpPost]
        public ActionResult doctorDutyList(string keyword, tbl_DoctorDuty model)
        {
            var results = (from item in db.tbl_DoctorDuty where item.Shift.Contains(keyword) || item.tbl_staffInfo.Name.Contains(keyword) select item).ToList();
            var PageIndex = model.Page ?? 1;
            model.doctorDutyListResults = results.ToPagedList(PageIndex, RecordPerPages);
            return View(model);
        }
        [HttpGet]
        public ActionResult doctordutyUpdate(int id = 0)
        {

            doctordutyId = id;
            tbl_DoctorDuty bc = db.tbl_DoctorDuty.FirstOrDefault(s => s.DoctorDutyID == doctordutyId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            var info = (from item in db.tbl_StaffType where item.StaffName == "Doctor" select item).FirstOrDefault();
            var docinfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            ViewBag.StaffID = new SelectList(docinfo, "StaffID", "Name");
            ViewBag.GetShift = bc.Shift;
            ViewBag.GetVisitTime = bc.VisitTime;
            ViewBag.GetOperationSchedule = bc.OperationDescription;
            ViewBag.GetOperationTime = bc.OperationTime;
            ViewBag.GetOperationDescription = bc.OperationDescription;
            return View(bc);
        }

        [HttpPost]
        public ActionResult doctordutyUpdate(tbl_DoctorDuty bc, int id = 0)
        {
            tbl_DoctorDuty bc1 = db.tbl_DoctorDuty.FirstOrDefault(s => s.DoctorDutyID == doctordutyId);
            bc1.StaffID = bc.StaffID;
            bc1.Shift = bc.Shift;
            bc1.VisitTime = bc.VisitTime;
            bc1.OperationSchedule = bc.OperationSchedule;
            bc1.OperationTime = bc.OperationTime;
            bc1.OperationDescription = bc.OperationDescription;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            var info = (from item in db.tbl_StaffType where item.StaffName == "Doctor" select item).FirstOrDefault();
            var docinfo = (from item in db.tbl_staffInfo where item.StaffTypeID == info.StaffTypeID select item).ToList();
            ViewBag.StaffID = new SelectList(docinfo, "StaffID", "Name");
            ViewBag.Messages = "Update Successfully";
            return View(bc1);
        }
        public ActionResult doctorDutyDelete(int id = 0)
        {
            tbl_DoctorDuty bc1 = db.tbl_DoctorDuty.FirstOrDefault(s => s.DoctorDutyID == id);
            db.tbl_DoctorDuty.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("doctorDutyList");
        }
	}
}