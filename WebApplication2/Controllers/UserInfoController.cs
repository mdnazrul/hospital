using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class UserInfoController : Controller
    {
        HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addUserInfo()
        {
            ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
            ViewBag.UserRoleID = new SelectList(db.tbl_UserRole, "UserRoleID", "UserRoleName");
            return View();
        }
        [HttpPost]
        public ActionResult addUserInfo(tbl_UserInfo userInfo, tbl_UserInfo userInfo2)
        {
            try
            {
                var encrypPass = Helpers.SHA1.Encode(userInfo.Password);
                userInfo2.UserName = userInfo.UserName;
                userInfo2.Password = encrypPass;
                db.tbl_UserInfo.Add(userInfo2);
                db.SaveChanges();
                ViewBag.StaffID = new SelectList(db.tbl_staffInfo, "StaffID", "Name");
                ViewBag.UserRoleID = new SelectList(db.tbl_UserRole, "UserRoleID", "UserRoleName");
                ViewBag.Message = "Save Succesfully";
                return View(userInfo);

            }
            catch
            {
                ViewBag.Message = "Save Failed";
                return View(userInfo);
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(tbl_UserInfo userInfo)
        {
            if (userInfo.UserName == "" && userInfo.Password == "") ModelState.AddModelError("", "Enter User Name & Password");
            else if (userInfo.UserName == "") ModelState.AddModelError("", "Enter User Name");
            else if (userInfo.Password == "") ModelState.AddModelError("","Enter password");
            else if(IsValid(userInfo.UserName,userInfo.Password))
            {
                var info=(from item in db.tbl_UserInfo where item.UserName == userInfo.UserName select item.UserInfoID).FirstOrDefault();
                HttpCookie myCookie = new HttpCookie("UserSettings");
                myCookie["UserName"] = userInfo.UserName;
                myCookie["userId"] = info.ToString();
                Response.Cookies.Add(myCookie);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("","Login Details are wrong!");
            }
            return View(userInfo);
        }

        private bool IsValid(string username, string userpassword)
        {
            string x = Helpers.SHA1.Encode(userpassword);
            bool IsValid = false;
            var user = db.tbl_UserInfo.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                if (user.Password == x)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public ActionResult LogOut(tbl_UserInfo userInfo)
        {
            string userSettings = "";
            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["UserName"] != null)
                { userSettings = Request.Cookies["UserSettings"]["UserName"]; }
            }

            Session.Clear();
            Session.Abandon();
            HttpCookie myCookie = new HttpCookie("UserSettings");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "UserInfo");
        }
	}
}