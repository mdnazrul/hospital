using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class OutdorPatientLabTestController : Controller
    {
        const int RecordPerPage = 3;
        public static int outdoorpatientlabtestId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult outdorpatientlabtest()
        {
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult outdorpatientlabtest(tbl_OutdorPatientLabTest outdorpatientlabtest)
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
                outdorpatientlabtest.EntryDate = DateTime.Today;
                outdorpatientlabtest.UserID = Convert.ToInt32(userid);
                db.tbl_OutdorPatientLabTest.Add(outdorpatientlabtest);
                db.SaveChanges();
                return RedirectToAction("outdorpatientlabtest");
            }
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserInfoID", "UserName");
            return View(outdorpatientlabtest);
        }
        public ActionResult outdorpatientlabtestLists(tbl_OutdorPatientLabTest outdorPatientlab)
        {
            var results = (from item in db.tbl_OutdorPatientLabTest select item).ToList().OrderBy(s => s.OutdorLabTestId);
            var pageIndex = outdorPatientlab.Page ?? 1;
            outdorPatientlab.outdorpatientlabtestListsResults = results.ToPagedList(pageIndex, RecordPerPage);
            return View(outdorPatientlab);
        }
        [HttpPost]
        public ActionResult outdorpatientlabtestLists(string keyword, tbl_OutdorPatientLabTest outdorPatientlab)
        {
            var results = (from item in db.tbl_OutdorPatientLabTest where item.OutdorName.Contains(keyword) || item.tbl_LabInfo.LabName.Contains(keyword) select item).ToList();
            var pageIndex = outdorPatientlab.Page ?? 1;
            outdorPatientlab.outdorpatientlabtestListsResults = results.ToPagedList(pageIndex, RecordPerPage);
            return View(outdorPatientlab);
        }
        [HttpGet]
        public ActionResult outdorpatientlabtestUpdate(int id = 0)
        {
            outdoorpatientlabtestId = id;
            tbl_OutdorPatientLabTest bc = db.tbl_OutdorPatientLabTest.FirstOrDefault(s => s.OutdorLabTestId == outdoorpatientlabtestId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetOutdorName = bc.OutdorName;
            ViewBag.GetAddress = bc.Address;
            ViewBag.GetMobNo = bc.MobNo;
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            return View(bc);

        }
        [HttpPost]
        public ActionResult outdorpatientlabtestUpdate(tbl_OutdorPatientLabTest bc, int id = 0)
        {
            tbl_OutdorPatientLabTest bc1 = db.tbl_OutdorPatientLabTest.FirstOrDefault(s => s.OutdorLabTestId == outdoorpatientlabtestId);
            bc1.OutdorName = bc.OutdorName;
            bc1.Address = bc.Address;
            bc1.MobNo = bc.MobNo;
            bc1.LabID = bc.LabID;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.LabID = new SelectList(db.tbl_LabInfo, "LabID", "LabName");
            ViewBag.Messages = "Update Succesfully";
            return View(bc1);
        }
        public ActionResult outdorpatientlabtestDelet(int id = 0)
        {
            tbl_OutdorPatientLabTest bc1 = db.tbl_OutdorPatientLabTest.FirstOrDefault(s => s.OutdorLabTestId == id);
            db.tbl_OutdorPatientLabTest.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("outdorpatientlabtestLists");
        }
	}
}