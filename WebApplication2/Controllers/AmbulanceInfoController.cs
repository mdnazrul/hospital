using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class AmbulanceInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int ambulanceId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addAmbulanceInfo()
        {
            ViewBag.AmbCatagoryID = new SelectList(db.tbl_AmbCatagory, "AmbCatagoryID", "AmbCatName");
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult addAmbulanceInfo(tbl_AmbulanceInfo ambulanceinfo)
        {
            if (ModelState.IsValid)
            {
                db.tbl_AmbulanceInfo.Add(ambulanceinfo);
                db.SaveChanges();
                return RedirectToAction("ambulanceInfoList");
            }
            ViewBag.AmbCatagoryID = new SelectList(db.tbl_AmbCatagory, "AmbCatagoryID", "AmbCatName");
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            return View(ambulanceinfo);
        }
        public ActionResult ambulanceInfoList(tbl_AmbulanceInfo model)
        {
            var results = (from item in db.tbl_AmbulanceInfo select item).ToList().OrderBy(p => p.AmbID);
            var pageIndex = model.Page ?? 1;
            model.ambulanceInfoListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);

        }

        [HttpPost]
        public ActionResult ambulanceInfoList(string keyword, tbl_AmbulanceInfo model)
        {
            var results = (from item in db.tbl_AmbulanceInfo where item.tbl_staffInfo.Name.Contains(keyword) || item.tbl_AmbCatagory.AmbCatName.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.ambulanceInfoListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpGet]
        public ActionResult ambulanceUpdate(int id = 0)
        {
            ambulanceId = id;
            tbl_AmbulanceInfo bc = db.tbl_AmbulanceInfo.FirstOrDefault(s => s.AmbID == ambulanceId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetAmbPlateNo = bc.AmbPlateNo;
            ViewBag.AmbCatagoryID = new SelectList(db.tbl_AmbCatagory, "AmbCatagoryID", "AmbCatName");
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.GetFreeOrBusy = bc.FreeOrBusy;          
            return View(bc);

        }
        [HttpPost]
        public ActionResult ambulanceUpdate(tbl_AmbulanceInfo bc, int id = 0)
        {
            tbl_AmbulanceInfo bc1 = db.tbl_AmbulanceInfo.FirstOrDefault(s => s.AmbID == ambulanceId);
            bc1.AmbPlateNo = bc.AmbPlateNo;
            bc1.AmbCatagoryID = bc.AmbCatagoryID;
            bc1.StaffID = bc.StaffID;
            bc1.FreeOrBusy = bc.FreeOrBusy;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.AmbCatagoryID = new SelectList(db.tbl_AmbCatagory, "AmbCatagoryID", "AmbCatName");
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.Message = "Update Sucessfully";
            return View(bc1);
        }
        public ActionResult ambulanceDelete(int id = 0)
        {
            tbl_AmbulanceInfo bc1 = db.tbl_AmbulanceInfo.FirstOrDefault(s => s.AmbID == id);
            db.tbl_AmbulanceInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("ambulanceInfoList");
        }
	}
}