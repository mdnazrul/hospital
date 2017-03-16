using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class LabInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int labinfoId = 0;
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addLabInfo()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult addLabInfo(tbl_LabInfo labinfo)
        {
           
            if (ModelState.IsValid)
            {
                db.tbl_LabInfo.Add(labinfo);
                db.SaveChanges();
                return RedirectToAction("labinfoList");
            }
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View(labinfo);

        }
        public ActionResult labinfoList(tbl_LabInfo model)
        {
            var results = (from item in db.tbl_LabInfo
                           select item).ToList().OrderBy(p => p.LabID);
            var pageIndex = model.Page ?? 1;
            model.labinfoListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpPost]
        public ActionResult labinfoList(string keyword, tbl_LabInfo model)
        {
            var results = (from item in db.tbl_LabInfo where item.Shift.Contains(keyword) || item.tbl_staffInfo.Name.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.labinfoListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult labInfoUpdate(int id = 0)
        {
            labinfoId = id;
            tbl_LabInfo bc = db.tbl_LabInfo.FirstOrDefault(s => s.LabID == labinfoId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetShift = bc.Shift;
            ViewBag.GetLabNo = bc.LabNo;
            ViewBag.GetLabName = bc.LabName;
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View(bc);
        }
        [HttpPost]
        public ActionResult labInfoUpdate(tbl_LabInfo bc, int id = 0)
        {
            tbl_LabInfo bc1 = db.tbl_LabInfo.FirstOrDefault(s => s.LabID == labinfoId);
            bc1.Shift = bc.Shift;
            bc1.LabNo = bc.LabNo;
            bc1.LabName = bc.LabName;
            bc1.StaffID = bc.StaffID;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.Message = "Update Succesfuly";
            return View(bc1);

        }
        public ActionResult labInfoDelete(int id = 0)
        {
            tbl_LabInfo bc1 = db.tbl_LabInfo.FirstOrDefault(s => s.LabID == id);
            db.tbl_LabInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("labinfoList");
        }

	}
}