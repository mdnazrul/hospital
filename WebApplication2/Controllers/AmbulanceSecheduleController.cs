using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class AmbulanceSecheduleController : Controller
    {
        const int RecordsPerPage = 3;
        public static int ambulanceScheduleId = 0;

        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addambulanceSechedule()
        {
            ViewBag.AmbID = new SelectList(db.tbl_AmbulanceInfo, "AmbID", "AmbPlateNo");
            return View();
        }
        [HttpPost]
        public ActionResult addambulanceSechedule(tbl_AmbulanceSchedule ambulanceSchedule)
        {
            if (ModelState.IsValid)
            {
                db.tbl_AmbulanceSchedule.Add(ambulanceSchedule);
                db.SaveChanges();
                return RedirectToAction("ambulanceScheduleLists");
            }
            ViewBag.AmbID = new SelectList(db.tbl_AmbulanceInfo, "AmbID", "AmbPlateNo");
            return View(ambulanceSchedule);
        }

        public ActionResult ambulanceScheduleLists(tbl_AmbulanceSchedule model)
        {
            var results = (from item in db.tbl_AmbulanceSchedule select item).ToList().OrderBy(p => p.AmbSchedulID);
            var pageIdnex = model.Page ?? 1;
            model.ambulanceScheduleListsResults = results.ToPagedList(pageIdnex, RecordsPerPage);
            return View(model);
        }

        [HttpPost]
        public ActionResult ambulanceScheduleLists(string keyword, tbl_AmbulanceSchedule model)
        {
            var results = (from item in db.tbl_AmbulanceSchedule where item.tbl_AmbulanceInfo.AmbPlateNo.Contains(keyword) || item.GoingTime.Contains(keyword) || item.BackTime.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.ambulanceScheduleListsResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult ambulanceSchedulesUpdate(int id = 0)
        {
            ambulanceScheduleId = id;
            tbl_AmbulanceSchedule bc = db.tbl_AmbulanceSchedule.FirstOrDefault(s => s.AmbSchedulID == ambulanceScheduleId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.AmbID = new SelectList(db.tbl_AmbulanceInfo, "AmbID", "AmbPlateNo");
            ViewBag.GetGoingTime = bc.GoingTime;
            ViewBag.GetBackTime = bc.BackTime;
            ViewBag.Date = bc.Date;
            ViewBag.GetFreeOrBusy = bc.FreeOrBusy;
            return View(bc);
        }
        [HttpPost]
        public ActionResult ambulanceSchedulesUpdate(tbl_AmbulanceSchedule bc, int id = 0)
        {
            tbl_AmbulanceSchedule bc1 = db.tbl_AmbulanceSchedule.FirstOrDefault(s => s.AmbSchedulID == ambulanceScheduleId);
            bc1.AmbID = bc.AmbID;
            bc1.GoingTime = bc.GoingTime;
            bc1.BackTime = bc.BackTime;
            bc1.Date = bc.Date;
            bc1.FreeOrBusy = bc.FreeOrBusy;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.AmbID = new SelectList(db.tbl_AmbulanceInfo, "AmbID", "AmbPlateNo");
            ViewBag.Messages = "Update Successfully";
            return View(bc1);
        }
        public ActionResult ambulanceSchedulesDelete(int id = 0)
        {
            tbl_AmbulanceSchedule bc1 = db.tbl_AmbulanceSchedule.FirstOrDefault(s => s.AmbSchedulID == id);
            db.tbl_AmbulanceSchedule.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("ambulanceScheduleLists");
        }
	}
}