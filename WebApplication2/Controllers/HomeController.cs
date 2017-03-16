using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult Index(tbl_UserInfo userInfo)
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                string userSettings = "";
                if (Request.Cookies["UserSettings"] !=null)
                {
                    if (Request.Cookies["UserSettings"]["UserName"] != null)
                    { userSettings = Request.Cookies["UserSettings"]["UserName"]; }
                }
                var info=(from item in db.tbl_UserInfo where item.UserName != userSettings orderby item.UserName select item).ToList();
                return View(info);
            }
            else
            {
                return RedirectToAction("LogIn", "UserInfo");
            }
            
        }    
    }
}