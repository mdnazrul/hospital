using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;
using PagedList;

namespace WebApplication2.Controllers
{
    public class StaffInformationController : Controller
    {
        const int RecordsPerPage = 3;
        public static int NameId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();

        public ActionResult addStaff()
        {
            ViewBag.StaffTypeID = new SelectList(db.tbl_StaffType, "StaffTypeID", "StaffName");
            ViewBag.DesignationID = new SelectList(db.tbl_DesignationType, "DesignationID", "DesingnationName");
            return View();
        }
        [HttpPost]
        public ActionResult addStaff(HttpPostedFileBase file, tbl_staffInfo ebi)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    ebi.Image = fileName;
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);
                }

                db.tbl_staffInfo.Add(ebi);
                db.SaveChanges();
                return RedirectToAction("staffList");
            }
            ViewBag.StaffTypeID = new SelectList(db.tbl_StaffType, "StaffTypeID", "StaffName");
            ViewBag.DesignationID = new SelectList(db.tbl_DesignationType, "DesignationID", "DesingnationName");
            return View(ebi);
        }
        public ActionResult staffList(tbl_staffInfo model)
        {
            var results = (from item in db.tbl_staffInfo
                           select item).ToList().OrderBy(p => p.StaffID);
            var pageIndex = model.Page ?? 1;
            model.staffListListsResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);

        }

        [HttpPost]
        public ActionResult staffList(string keyword, tbl_staffInfo model)
        {
            var results = (from item in db.tbl_staffInfo where item.Name.Contains(keyword) || item.tbl_StaffType.StaffName.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.staffListListsResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult staffUpdate(int id = 0)
        {
            NameId = id;
            tbl_staffInfo bc = db.tbl_staffInfo.FirstOrDefault(s => s.StaffID == NameId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetName = bc.Name;

            ViewBag.StaffTypeID = new SelectList(db.tbl_StaffType, "StaffTypeID", "StaffName");

            ViewBag.DesignationID = new SelectList(db.tbl_DesignationType, "DesignationID", "DesingnationName");
            ViewBag.GetPresentAddress = bc.PresentAddress;
            ViewBag.GetMobNo = bc.MobNo;
            ViewBag.GetImage = bc.Image;
            //ViewBag.GetJoiningDate = bc.JoiningDate;
            ViewBag.GetEmailNo = bc.EmailNo;
            return View(bc);
        }
        [HttpPost]
        public ActionResult staffUpdate(tbl_staffInfo bc, int id = 0)
        {
            tbl_staffInfo bc1 = db.tbl_staffInfo.FirstOrDefault(s => s.StaffID == NameId);
            bc1.Name = bc.Name;
            bc1.StaffTypeID = bc.StaffTypeID;
            bc1.DesignationID = bc.DesignationID;
            bc1.PresentAddress = bc.PresentAddress;
            bc1.MobNo = bc.MobNo;
            //bc1.JoiningDate = bc.JoiningDate;
            bc1.EmailNo = bc.EmailNo;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.StaffTypeID = new SelectList(db.tbl_StaffType, "StaffTypeID", "StaffName");
            ViewBag.DesignationID = new SelectList(db.tbl_DesignationType, "DesignationID", "DesingnationName");
            ViewBag.Message = "Update Successfuly";
            return View(bc1);
        }
        public ActionResult DeleteData(int id = 0)
        {
            tbl_staffInfo bc1 = db.tbl_staffInfo.FirstOrDefault(x => x.StaffID == id);
            db.tbl_staffInfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("staffList");
        }
	}
}