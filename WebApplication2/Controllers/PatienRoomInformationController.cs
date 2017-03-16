using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;


namespace WebApplication2.Controllers
{
    public class PatienRoomInformationController : Controller
    {
        const int RecordsPerPage = 3;
        public static int pationRoomInfoId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addPatientRoomInformation()
        {
            ViewBag.CategorayID = new SelectList(db.tbl_RoomCatagory, "CategorayID", "CategoryName");
            ViewBag.RoomTypeID = new SelectList(db.tbl_RoomType, "RoomTypeID", "RoomTypeName");
            return View();
        }
        [HttpPost]
        public ActionResult addPatientRoomInformation(tbl_PationRoomInformation pationroomInfo)
        {
            if (ModelState.IsValid)
            {
                db.tbl_PationRoomInformation.Add(pationroomInfo);
                db.SaveChanges();
                return RedirectToAction("pationRoomInformationList");
            }
            ViewBag.CategorayID = new SelectList(db.tbl_RoomCatagory, "CategorayID", "CategoryName");
            ViewBag.RoomTypeID = new SelectList(db.tbl_RoomType, "RoomTypeID", "RoomTypeName");
            return View(pationroomInfo);
        }

        public ActionResult pationRoomInformationList(tbl_PationRoomInformation model)
        {
            var results = (from item in db.tbl_PationRoomInformation
                           select item).ToList().OrderBy(p => p.PRoomID);
            var pageIndex = model.Page ?? 1;
            model.pationRoomInformationListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpPost]
        public ActionResult pationRoomInformationList(string keyword, tbl_PationRoomInformation model)
        {
            var results = (from item in db.tbl_PationRoomInformation where item.tbl_RoomCatagory.CategoryName.Contains(keyword) || item.tbl_RoomType.RoomTypeName.Contains(keyword) select item).ToList();
            var pageIndex = model.Page ?? 1;
            model.pationRoomInformationListResults = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(model);
        }

        [HttpGet]
        public ActionResult patientRoomInfoUpdate(int id = 0)
        {
            pationRoomInfoId = id;
            tbl_PationRoomInformation bc = db.tbl_PationRoomInformation.FirstOrDefault(s => s.PRoomID == pationRoomInfoId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetProomNo = bc.ProomNo;
            ViewBag.CategorayID = new SelectList(db.tbl_RoomCatagory, "CategorayID", "CategoryName");
            ViewBag.RoomTypeID = new SelectList(db.tbl_RoomType, "RoomTypeID", "RoomTypeName");
            ViewBag.GetFloorNo = bc.FloorNo;
            ViewBag.GetAmount = bc.Amount;
            return View(bc);
        }
        [HttpPost]
        public ActionResult patientRoomInfoUpdate(tbl_PationRoomInformation bc, int id = 0)
        {
            tbl_PationRoomInformation bc1 = db.tbl_PationRoomInformation.FirstOrDefault(s => s.PRoomID == pationRoomInfoId);
            bc1.ProomNo = bc.ProomNo;
            bc1.CategorayID = bc.CategorayID;
            bc1.RoomTypeID = bc.RoomTypeID;
            bc1.FloorNo = bc.FloorNo;
            bc1.Amount = bc.Amount;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.CategorayID = new SelectList(db.tbl_RoomCatagory, "CategorayID", "CategoryName");
            ViewBag.RoomTypeID = new SelectList(db.tbl_RoomType, "RoomTypeID", "RoomTypeName");
            ViewBag.Message = "Update Successully";
            return View(bc1);

        }
        public ActionResult patientRoomDelete(int id = 0)
        {
            tbl_PationRoomInformation bc1 = db.tbl_PationRoomInformation.FirstOrDefault(s => s.PRoomID == id);
            db.tbl_PationRoomInformation.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("pationRoomInformationList");
        }
	}
}