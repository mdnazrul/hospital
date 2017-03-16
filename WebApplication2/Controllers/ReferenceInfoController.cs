using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;
namespace WebApplication2.Controllers
{
    public class ReferenceInfoController : Controller
    {

        const int RecordsPerPage = 3;
        public static int referId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addReferenceInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addReferenceInfo(tbl_Referenceinfo referenceinfo)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Referenceinfo.Add(referenceinfo);
                db.SaveChanges();
                return RedirectToAction("referenceList");
            }
            return View(referenceinfo);
        }
        public ActionResult referenceList(tbl_Referenceinfo model)
        {
            var results = (from item in db.tbl_Referenceinfo
                           select item).ToList().OrderBy(p => p.ReferenceID);
            var pageIndex = model.Page ?? 1;
            model.referenceListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }
        [HttpPost]
        public ActionResult referenceList(string keyword, tbl_Referenceinfo model)
        {
            var results = (from item in db.tbl_Referenceinfo where item.ReferenceName.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.referenceListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult referenceUpdate(int id = 0)
        {
            referId = id;
            tbl_Referenceinfo bc = db.tbl_Referenceinfo.FirstOrDefault(s => s.ReferenceID == referId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetReferenceName = bc.ReferenceName;
            ViewBag.GetAddress = bc.Address;
            ViewBag.GetPhoneNo = bc.PhoneNo;
            ViewBag.GetAgreementInfo = bc.AgreementInfo;
            return View(bc);
        }
        [HttpPost]
        public ActionResult referenceUpdate(tbl_Referenceinfo bc, int id = 0)
        {
            tbl_Referenceinfo bc1 = db.tbl_Referenceinfo.FirstOrDefault(x => x.ReferenceID == referId);
            bc1.ReferenceName = bc.ReferenceName;
            bc1.Address = bc.Address;
            bc1.PhoneNo = bc.PhoneNo;
            bc1.AgreementInfo = bc.AgreementInfo;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.Message = "Update Successfully";
            return View(bc1);
        }
        public ActionResult referenceDelete(int id = 0)
        {
            tbl_Referenceinfo bc1 = db.tbl_Referenceinfo.FirstOrDefault(x => x.ReferenceID == id);
            db.tbl_Referenceinfo.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("referenceList");
        }

	}
}