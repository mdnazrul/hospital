using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class BloodSupplierInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int BSupplierId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult SupplierInfo()
        {
            return View();

        }
        [HttpPost]
        public ActionResult SupplierInfo(tbl_BloodSupplierInfo supplier)
        {
            if (ModelState.IsValid)
            {
                db.tbl_BloodSupplierInfo.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("bloodsupplierList");
            }
            return View(supplier);
        }
        public ActionResult bloodsupplierList(tbl_BloodSupplierInfo model)
        {
            var results = (from item in db.tbl_BloodSupplierInfo
                           select item).ToList().OrderBy(p => p.BloodSupplyerID);
            var pageIndex = model.Page ?? 1;
            model.bloodsupplierListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult bloodsupplierList(string keyword, tbl_BloodSupplierInfo model)
        {
            var results = (from item in db.tbl_BloodSupplierInfo where item.CompanyName.Contains(keyword) || item.ContactPerson.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.bloodsupplierListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpGet]
        public ActionResult BSupplierUpdate(int id = 0)
        {
            BSupplierId = id;
            tbl_BloodSupplierInfo bc = db.tbl_BloodSupplierInfo.FirstOrDefault(s => s.BloodSupplyerID == BSupplierId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetCompanyName = bc.CompanyName;
            ViewBag.GetComAddress = bc.ComAddress;
            ViewBag.GetComMobNo = bc.ComMobNo;
            ViewBag.GetContactPerson = bc.ContactPerson;
            return View(bc);
        }
        [HttpPost]
        public ActionResult BSupplierUpdate(tbl_BloodSupplierInfo bc, int id = 0)
        {
            tbl_BloodSupplierInfo bc1 = db.tbl_BloodSupplierInfo.FirstOrDefault(s => s.BloodSupplyerID == BSupplierId);
            bc1.CompanyName = bc.CompanyName;
            bc1.ComAddress = bc.ComAddress;
            bc1.ComMobNo = bc.ComMobNo;
            bc1.ContactPerson = bc.ContactPerson;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.Message = "Update Successfully";
            return View(bc1);
        }
        public ActionResult BSupplierDelete(int id = 0)
        {
            tbl_BloodSupplierInfo bc1 = db.tbl_BloodSupplierInfo.FirstOrDefault(s => s.BloodSupplyerID == id);
            db.tbl_BloodSupplierInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("bloodsupplierList");
        }
	}
}