using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class IndorPatientLabTestController : Controller
    {
        const int RecordsPerPages = 3;
        public static int indorpatientlabtestId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addIndorPationtLabTest()
        {
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult addIndorPationtLabTest(Tbl_IndorPatientLabTest indorpatientlabTest)
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
                indorpatientlabTest.EntryDate = DateTime.Today;
                indorpatientlabTest.UserID = Convert.ToInt32(userid);
                db.Tbl_IndorPatientLabTest.Add(indorpatientlabTest);
                db.SaveChanges();
                return RedirectToAction("indorpatientlabtestLists");
            }
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(indorpatientlabTest);
        }

        public ActionResult indorpatientlabtestLists(Tbl_IndorPatientLabTest indorpatientlabtest)
        {
            var results = (from item in db.Tbl_IndorPatientLabTest select item).ToList().OrderBy(s => s.LabTestID);
            var pageIndex = indorpatientlabtest.Page ?? 1;
            indorpatientlabtest.indorpatientlabtestListsResults = results.ToPagedList(pageIndex, RecordsPerPages);
            return View(indorpatientlabtest);

        }
        [HttpPost]
        public ActionResult indorpatientlabtestLists(string keyword, Tbl_IndorPatientLabTest indorpatientlabtest)
        {
            var results = (from item in db.Tbl_IndorPatientLabTest where item.tbl_PatientInfo.PatientName.Contains(keyword) || item.tbl_LabInfo.LabName.Contains(keyword) select item).ToList();
            var pageIndex = indorpatientlabtest.Page ?? 1;
            indorpatientlabtest.indorpatientlabtestListsResults = results.ToPagedList(pageIndex, RecordsPerPages);
            return View(indorpatientlabtest);
        }

        [HttpGet]
        public ActionResult indorpatientlabtestUpdate(int id = 0)
        {
            indorpatientlabtestId = id;
            Tbl_IndorPatientLabTest bc = db.Tbl_IndorPatientLabTest.FirstOrDefault(s => s.LabTestID == indorpatientlabtestId);
            if (bc == null)
            {
                return HttpNotFound();

            }
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.GetPurpose = bc.Purpose;
            return View(bc);
        }
        [HttpPost]
        public ActionResult indorpatientlabtestUpdate(Tbl_IndorPatientLabTest bc, int id = 0)
        {
            Tbl_IndorPatientLabTest bc1 = db.Tbl_IndorPatientLabTest.FirstOrDefault(s => s.LabTestID == indorpatientlabtestId);
            bc1.PatientID = bc.PatientID;
            bc1.LabID = bc.LabID;
            bc1.Purpose = bc.Purpose;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.PatientID = new SelectList(db.tbl_PatientInfo, "PatientID", "PatientName");
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.Message = "Update Succesfully";
            return View(bc1);
        }
        public ActionResult indorpatientlabtestDelete(int id = 0)
        {
            Tbl_IndorPatientLabTest bc1 = db.Tbl_IndorPatientLabTest.FirstOrDefault(s => s.LabTestID == id);
            db.Tbl_IndorPatientLabTest.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("indorpatientlabtestLists");
        }
	}
}