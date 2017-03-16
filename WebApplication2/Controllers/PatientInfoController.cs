using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class PatientInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int patientInfoId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addPatientInfo()
        {
            ViewBag.ReferenceID = new SelectList(db.tbl_Referenceinfo, "ReferenceID", "ReferenceName");
            ViewBag.PRoomID = new SelectList(db.tbl_PationRoomInformation, "PRoomID", "ProomNo");

            return View();
        }
        [HttpPost]
        public ActionResult addPatientInfo(tbl_PatientInfo patientinfo)
        {

            if (ModelState.IsValid)
            {
                db.tbl_PatientInfo.Add(patientinfo);
                db.SaveChanges();
                return RedirectToAction("patientInfoList");
            }
            ViewBag.ReferenceID = new SelectList(db.tbl_Referenceinfo, "ReferenceID", "ReferenceName");
            ViewBag.PRoomID = new SelectList(db.tbl_PationRoomInformation, "PRoomID", "ProomNo");
            return View(patientinfo);
        }
        public ActionResult patientInfoList(tbl_PatientInfo model)
        {
            var results = (from item in db.tbl_PatientInfo
                           select item).ToList().OrderBy(p => p.PatientID);
            var pageIndex = model.Page ?? 1;
            model.patientInfoListResult = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult patientInfoList(string keyword, tbl_PatientInfo model)
        {
            var results = (from item in db.tbl_PatientInfo where item.PatientName.Contains(keyword) || item.Gender.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.patientInfoListResult = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult patientInfoUpdate(int id = 0)
        {
            patientInfoId = id;
            tbl_PatientInfo bc = db.tbl_PatientInfo.FirstOrDefault(s => s.PatientID == patientInfoId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetPatientName = bc.PatientName;
            ViewBag.GetGender = bc.Gender;
            ViewBag.Disease = bc.Disease;
            ViewBag.ReferenceID = new SelectList(db.tbl_Referenceinfo, "ReferenceID", "ReferenceName");
            ViewBag.GetGardian = bc.Gardian;
            ViewBag.PRoomID = new SelectList(db.tbl_PationRoomInformation, "PRoomID", "ProomNo");
            return View(bc);

        }
        [HttpPost]
        public ActionResult patientInfoUpdate(tbl_PatientInfo bc, int id = 0)
        {
            tbl_PatientInfo bc1 = db.tbl_PatientInfo.FirstOrDefault(s => s.PatientID == patientInfoId);
            bc1.PatientName = bc.PatientName;
            bc1.Gender = bc.Gender;
            bc1.Disease = bc.Disease;
            ViewBag.ReferenceID = new SelectList(db.tbl_Referenceinfo, "ReferenceID", "ReferenceName");
            bc1.Gardian = bc.Gardian;
            ViewBag.PRoomID = new SelectList(db.tbl_PationRoomInformation, "PRoomID", "ProomNo");
            db.SaveChanges();
            ViewBag.Message = "Update Successfully";
            return View(bc1);

        }
        public ActionResult patientInfoDelete(int id = 0)
        {
            tbl_PatientInfo bc1 = db.tbl_PatientInfo.FirstOrDefault(s => s.PatientID == id);
            db.tbl_PatientInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("patientInfoList");
        }
	}
}