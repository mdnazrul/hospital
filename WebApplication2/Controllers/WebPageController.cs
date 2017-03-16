using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class WebPageController : Controller
    {
        //
        // GET: /WebPage/
       public ActionResult showpage()
        {
            return RedirectToAction("LogIn", "UserInfo");
        }
	}
}