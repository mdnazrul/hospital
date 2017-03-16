using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class UserRoleController : Controller
    {
        public static int userRolId = 0;
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addUserRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addUserRole(tbl_UserRole userRole)
        {           
            if (ModelState.IsValid)
            {
                db.tbl_UserRole.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("userRoleList");
            }
            return View(userRole);
        }
        public ActionResult userRoleList()
        {
            return View(db.tbl_UserRole.ToList());
        }
        [HttpGet]
        public ActionResult userRoleUpdate(int id = 0)
        {
            userRolId = id;
            tbl_UserRole bc = db.tbl_UserRole.FirstOrDefault(s => s.UserRoleID == userRolId);
            if (bc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GetUserRoleName = bc.UserRoleName;
            return View(bc);
        }
        [HttpPost]
        public ActionResult userRoleUpdate(tbl_UserRole bc, int id = 0)
        {
            tbl_UserRole bc1 = db.tbl_UserRole.FirstOrDefault(s => s.UserRoleID == userRolId);
            bc1.UserRoleName = bc.UserRoleName;
            if (bc1 == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            ViewBag.Message = "Update Succesfully";
            return View(bc1);
        }
        public ActionResult userRoleDelete(int id = 0)
        {
            tbl_UserRole bc1 = db.tbl_UserRole.FirstOrDefault(s => s.UserRoleID == id);
            db.tbl_UserRole.Remove(bc1);
            db.SaveChanges();
            return RedirectToAction("userRoleList");
        }
	}
}