using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class MedicineSupplierInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int medicnSupplierId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addMsupplierinfo()
        {
            //ViewBag.MediUnitID = new SelectList(db.MedicinUnits, "MediUnitID", "UnitName");
            return View();
        }
        [HttpPost]
        public ActionResult addMsupplierinfo(tbl_MedicineSupplierInfo medicinSupInfo)
        {
            if (ModelState.IsValid)
            {
                db.tbl_MedicineSupplierInfo.Add(medicinSupInfo);
                db.SaveChanges();
                return RedirectToAction("medicineSupplierList");
            }
            return View(medicinSupInfo);
        }
        public ActionResult medicineSupplierList(tbl_MedicineSupplierInfo model)
        {
            var results = (from item in db.tbl_MedicineSupplierInfo
                           select item).ToList().OrderBy(p => p.MediSupplyerID);
            var pageIndex = model.Page ?? 1;
            model.medicineSupplierListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult medicineSupplierList(string keyword, tbl_MedicineSupplierInfo model)
        {
            var results = (from item in db.tbl_MedicineSupplierInfo where item.MediSupplyerName.Contains(keyword) || item.ContractPerson.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.medicineSupplierListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult medicineSupplierUpdate(int id = 0)
        {
            medicnSupplierId = id;
            tbl_MedicineSupplierInfo bc = db.tbl_MedicineSupplierInfo.FirstOrDefault(s => s.MediSupplyerID == medicnSupplierId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetMediSupplyerName = bc.MediSupplyerName;
            ViewBag.GetComMobNo = bc.ComMobNo;
            ViewBag.GetContractPerson = bc.ContractPerson;
            ViewBag.GetContractPersMobNo = bc.ContractPersMobNo;
            ViewBag.GetEmail = bc.Email;
            //ViewBag.GetQuantity = bc.Quantity;
            return View(bc);

        }
        [HttpPost]
        public ActionResult medicineSupplierUpdate(tbl_MedicineSupplierInfo bc, int id = 0)
        {
            tbl_MedicineSupplierInfo bc1 = db.tbl_MedicineSupplierInfo.FirstOrDefault(s => s.MediSupplyerID == medicnSupplierId);
            bc1.MediSupplyerName = bc.MediSupplyerName;
            bc1.ComMobNo = bc.ComMobNo;
            bc1.ContractPerson = bc.ContractPerson;
            bc1.ContractPersMobNo = bc.ContractPersMobNo;
            bc1.Email = bc.Email;
            //bc1.Quantity = bc.Quantity;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.Message = "Update Successfully";
            return View(bc1);

        }

        public ActionResult medicinSupplierDelete(int id = 0)
        {
            tbl_MedicineSupplierInfo bc1 = db.tbl_MedicineSupplierInfo.FirstOrDefault(s => s.MediSupplyerID == id);
            db.tbl_MedicineSupplierInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("medicineSupplierList");
        }
	}
}