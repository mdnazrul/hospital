using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class MedicineInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int medicinId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addmedicineInfo()
        {
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.MediCataID = new SelectList(db.tbl_MedicinCatagory, "MediCataID", "CatagoryName");
            return View();
        }
        [HttpPost]
        public ActionResult addmedicineInfo(tbl_MedicineInfo medicineInfo)
        {

            if (ModelState.IsValid)
            {
                db.tbl_MedicineInfo.Add(medicineInfo);
                db.SaveChanges();
                return RedirectToAction("medicineInfoList");
            }
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.MediCataID = new SelectList(db.tbl_MedicinCatagory, "MediCataID", "CatagoryName");
            return View(medicineInfo);
        }
        public ActionResult medicineInfoList(tbl_MedicineInfo model)
        {
            var results = (from item in db.tbl_MedicineInfo
                           select item).ToList().OrderBy(p => p.MediInfoID);
            var pageIndex = model.Page ?? 1;
            model.medicineInfoListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult medicineInfoList(string keyword, tbl_MedicineInfo model)
        {
            var results = (from item in db.tbl_MedicineInfo where item.MediName.Contains(keyword) || item.MedicompaName.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.medicineInfoListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult medcinUpdate(int id = 0)
        {
            medicinId = id;
            tbl_MedicineInfo bc = db.tbl_MedicineInfo.FirstOrDefault(s => s.MediInfoID == medicinId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetMediName = bc.MediName;
            ViewBag.GetMedicompaName = bc.MedicompaName;
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.MediCataID = new SelectList(db.tbl_MedicinCatagory, "MediCataID", "CatagoryName");
            return View(bc);

        }
        [HttpPost]
        public ActionResult medcinUpdate(tbl_MedicineInfo bc, int id = 0)
        {
            tbl_MedicineInfo bc1 = db.tbl_MedicineInfo.FirstOrDefault(s => s.MediInfoID == medicinId);
            bc1.MediName = bc.MediName;
            bc1.MedicompaName = bc.MedicompaName;
            bc1.MediUnitID = bc.MediUnitID;
            bc1.MediCataID = bc.MediCataID;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.MediUnitID = new SelectList(db.tbl_MedicinUnit, "MediUnitID", "UnitName");
            ViewBag.MediCataID = new SelectList(db.tbl_MedicinCatagory, "MediCataID", "CatagoryName");
            ViewBag.Message = "Update Successfuly";
            return View(bc1);
        }
        public ActionResult medicinDelete(int id = 0)
        {
           tbl_MedicineInfo bc1 = db.tbl_MedicineInfo.FirstOrDefault(s => s.MediInfoID == id);
            db.tbl_MedicineInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("medicineInfoList");
        }
	}
}