using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class BloodDonnerInfoController : Controller
    {
        const int RecordsPerPage = 3;
        public static int NameId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult AddBDoner()
        {
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            return View();
        }
        [HttpPost]
        public ActionResult AddBDoner(tbl_BloodDonerInfo Bdonner)
        {

            if (ModelState.IsValid)
            {
                db.tbl_BloodDonerInfo.Add(Bdonner);
                db.SaveChanges();
                return RedirectToAction("AddDonerList");
            }
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            return View(Bdonner);     
        }
        public ActionResult AddDonerList(tbl_BloodDonerInfo model)
        {
            var results = (from item in db.tbl_BloodDonerInfo
                           select item).ToList().OrderBy(p => p.BloodDonerID);
            var pageIndex = model.Page ?? 1;
            model.AddDonerListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddDonerList(string keyword, tbl_BloodDonerInfo model)
        {
            var results = (from item in db.tbl_BloodDonerInfo where item.BloodDonerName.Contains(keyword) || item.tbl_BloodGroup.BloodName.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.AddDonerListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateData(int id = 0)
        {
            NameId = id;
            tbl_BloodDonerInfo bc = db.tbl_BloodDonerInfo.FirstOrDefault(x => x.BloodDonerID == NameId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetBloodDonerName = bc.BloodDonerName;
            ViewBag.GetAge = bc.Age;
            ViewBag.GetAddress = bc.Address;
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.GetNationalId = bc.NationalId;
            return View(bc);
        }
        [HttpPost]
        public ActionResult UpdateData(tbl_BloodDonerInfo bc, int id = 0)
        {
            tbl_BloodDonerInfo bc1 = db.tbl_BloodDonerInfo.FirstOrDefault(x => x.BloodDonerID == NameId);
            bc1.BloodDonerName = bc.BloodDonerName;
            bc1.Age = bc.Age;
            bc1.Address = bc.Address;
            bc1.BloodID = bc.BloodID;
            bc1.NationalId = bc.NationalId;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.BloodID = new SelectList(db.tbl_BloodGroup, "BloodID", "BloodName");
            ViewBag.Message = "Update Successfuly";
            return View(bc1);
        }
        public ActionResult DeleteData(int id = 0)
        {
            tbl_BloodDonerInfo bc1 = db.tbl_BloodDonerInfo.FirstOrDefault(x => x.BloodDonerID == id);
            db.tbl_BloodDonerInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("AddDonerList");
        }
	}
}