using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList;

namespace WebApplication2.Controllers
{
    public class ClaimController : Controller
    {
        const int RecordsPerPage = 3;
        public static int claimid = 0;

        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();
        public ActionResult addClaim()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addClaim(tbl_Claim claim)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Claim.Add(claim);
                db.SaveChanges();
                return RedirectToAction("claimList");
            }
            return View(claim);
        }
        public ActionResult claimList( tbl_Claim claim)
        {
            var results = (from item in db.tbl_Claim
                           select item).ToList().OrderBy(p => p.ClaimID);
            var pageIndex = claim.Page ?? 1;
            claim.claimListResult = results.ToPagedList(pageIndex, RecordsPerPage);          
            return View(claim);
        
        }
        [HttpPost]
        public ActionResult claimList(string keyword, tbl_Claim claim)
        {
            var results = (from item in db.tbl_Claim where item.CompanyName.Contains(keyword)|| item.ContactPerson.Contains(keyword) select item).ToList();
            var pageIndex = claim.Page ?? 1;
            claim.claimListResult = results.ToPagedList(pageIndex, RecordsPerPage);
            return View(claim);
        }
	}
}