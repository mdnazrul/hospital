using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;

namespace WebApplication2.Controllers
{
    public class SettingsController : Controller
    {
        private HospitalManagementSystemDBEntities db = new HospitalManagementSystemDBEntities();

        //
        // GET: /Settings/
        public static string fileName;
        public string GrabLogo;
        string User_Name = "";
        public ActionResult Index()
        {
            return View();
        }

        // Start Company Setup All Methods
        public ActionResult HospitalSetup()
        {

            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["User_Name"] != null)
                {
                    User_Name = Request.Cookies["UserSettings"]["User_Name"];
                }
                return View(db.HR_HospitalSetup.ToList());
            }
            else
            {
                return RedirectToAction("LogIn", "UserInfo");
            }

        }

        public ActionResult HospitalSetupList()
        {
            return PartialView(db.HR_HospitalSetup.ToList());
        }

        public JsonResult Company_Info(HR_HospitalSetup hrst, string CompanyName, string Address, string ContactNo, string Fax, string Email, string Web, string CompanyLogo)
        {
            var Info = (from item in db.HR_HospitalSetup select item).ToList();
            if (Info.Count > 0)
            {
                var at = Info.FirstOrDefault();
                hrst = db.HR_HospitalSetup.FirstOrDefault(x => x.CompanyId == at.CompanyId);
                hrst.CompanyName = CompanyName;
                hrst.Company_Address = Address;
                hrst.Company_ContactNo = ContactNo;
                hrst.Company_Fax = Fax;
                hrst.Company_Email = Email;
                hrst.Company_Web = Web;
                var usertypeInfoes = from item in db.HR_HospitalSetup
                                     where item.CompanyId == at.CompanyId
                                     select new { CompanyLogo = item.Company_Logo };
                foreach (var row in usertypeInfoes)
                {
                    GrabLogo = row.CompanyLogo;
                }
                if (CompanyLogo == "")
                {
                    //hrst.CompanyLogo = CompanyLogo; 
                    hrst.Company_Logo = GrabLogo;
                }
                else
                {
                    hrst.Company_Logo = CompanyLogo;
                }
                db.SaveChanges();
            }
            else
            {
                hrst.CompanyName = CompanyName;
                hrst.Company_Address = Address;
                hrst.Company_ContactNo = ContactNo;
                hrst.Company_Fax = Fax;
                hrst.Company_Email = Email;
                hrst.Company_Web = Web;
                hrst.Company_Logo = CompanyLogo;
                db.HR_HospitalSetup.Add(hrst);
                db.SaveChanges();
            }
            var jsonData = new { success = false, message = "Company Setup Successfully!" };
            return Json(jsonData);
        }

        public JsonResult UpdateCompany_Info(HR_HospitalSetup hrst, int Id, string CompanyName, string Address, string ContactNo, string Fax, string Email, string Web, string CompanyLogo)
        {
            hrst = db.HR_HospitalSetup.FirstOrDefault(x => x.CompanyId == Id);
            hrst.CompanyName = CompanyName;
            hrst.Company_Address = Address;
            hrst.Company_ContactNo = ContactNo;
            hrst.Company_Fax = Fax;
            hrst.Company_Email = Email;
            hrst.Company_Web = Web;

            var usertypeInfoes = from item in db.HR_HospitalSetup
                                 where item.CompanyId == Id
                                 select new { CompanyLogo = item.Company_Logo };
            foreach (var row in usertypeInfoes)
            {
                GrabLogo = row.CompanyLogo;
            }
            if (CompanyLogo == "")
            {
                //hrst.CompanyLogo = CompanyLogo; 
                hrst.Company_Logo = GrabLogo;
            }
            else
            {
                hrst.Company_Logo = CompanyLogo;
            }
            db.SaveChanges();
            var jsonData = new { success = false, message = "Company Setup updated Successfully!" };
            return Json(jsonData);
        }

        public JsonResult SaveImageToImages(HttpPostedFileBase file, int id)
        {
            var usertypeInfoes = from item in db.HR_HospitalSetup
                                 where item.CompanyId == id
                                 select new { CompanyLogo = item.Company_Logo };
            foreach (var row in usertypeInfoes)
            {
                GrabLogo = row.CompanyLogo;
            }

            if (id != 0)
            {
                System.IO.File.Delete(Request.PhysicalApplicationPath + "/Content/images/" + GrabLogo);
            }

            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                var jsonData = new { success = true, CompanyLogo = fileName };
                return Json(jsonData);
            }
            else
            {
                var jsonData = new { success = false, errormessage = "File not Uploaded" };
                return Json(jsonData);
            }
        }


        // End Company Setup All Methods
        Decimal Basic, Houserent, Conveyance, MedicalAllowance;
        public JsonResult SetEmpSalary(Decimal gross_salary)
        {
            Basic = (gross_salary * 50) / 100;
            Houserent = (gross_salary * 30) / 100;
            Conveyance = (gross_salary * 10) / 100;
            MedicalAllowance = (gross_salary * 10) / 100;

            var jsonData = new { success = true, message = "Done", Basic = Basic, Houserent = Houserent, Conveyance = Conveyance, MedicalAllowance = MedicalAllowance };
            return Json(jsonData);
        }
        //public ActionResult DesigList()
        //{
        //    return PartialView(db.HR_DesiTempName.ToList());
        //}
        //public ActionResult DesignationTempName()
        //{

        //    return View(db.HR_DesiTempName.ToList());
        //}
        //public JsonResult AddDesignationName(HR_DesiTempName dest, string DesigName)
        //{
        //    var Info = (from item in db.HR_DesiTempName where item.DesigName == DesigName select item).ToList();
        //    if (Info.Count > 0)
        //    {
        //        var jsonData = new { success = false, message = "Designation Name already exists." };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        dest.DesigName = DesigName;
        //        db.HR_DesiTempName.Add(dest);
        //        db.SaveChanges();
        //        var jsonData = new { success = true, message = "Designation Name Added Successfully" };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);


        //    }

        //}
        //public JsonResult UpdateDesignationTemp(HR_DesiTempName dest, int id, string DesigName)
        //{
        //    var Info = (from item in db.HR_DesiTempName where item.DesigName == DesigName select item).ToList();
        //    if (Info.Count > 0)
        //    {
        //        var jsonData = new { success = false, message = "Designation Name already exists." };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        dest = db.HR_DesiTempName.FirstOrDefault(x => x.id == id);
        //        dest.DesigName = DesigName;
        //        db.SaveChanges();
        //        var jsonData = new { success = true, message = "Designation Name Updated Successfully" };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //public JsonResult DeleteDesi(HR_DesiTempName dest, int id)
        //{
        //    dest = db.HR_DesiTempName.FirstOrDefault(x => x.id == id);
        //    db.HR_DesiTempName.Remove(dest);
        //    db.SaveChanges();
        //    var jsonData = new { success = true, message = "Designation Name Deleted Successfully" };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}


        //// Start Designation All Methods
        //public ActionResult DesignationSetup()
        //{
        //    if (Request.Cookies["UserSettings"] != null)
        //    {
        //        if (Request.Cookies["UserSettings"]["User_Name"] != null)
        //        {
        //            User_Name = Request.Cookies["UserSettings"]["User_Name"];
        //        }
        //        ViewBag.DesiTemp = new SelectList(db.HR_DesiTempName, "DesigName", "DesigName");
        //        return View(db.HR_Designation.ToList());
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //}

        //public ActionResult DesignationList()
        //{

        //    return PartialView(db.HR_Designation.ToList());
        //}
        //private int GetDCode;
        //private string GetDCode1;
        //public JsonResult AddDesignation(HR_Designation dg, string Designation, string Grade, decimal GrossSalary, decimal BasicSalary, decimal HomeRent, decimal FoodAllowance, decimal Medical, decimal Conveyance)
        //{
        //    var Info = (from item in db.HR_Designation where item.Designation == Designation && item.Grade == Grade && item.GrossSalary == GrossSalary && item.BasicSalary == BasicSalary && item.HomeRent == HomeRent && item.MedicalAllowance == Medical && item.Conveyance == Conveyance select item).ToList();
        //    if (Info.Count > 0)
        //    {
        //        var jsonData = new { success = false, message = "Designation already exists." };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {

        //        dg.Designation = Designation;
        //        var DesignationInfoes = (from item in db.HR_Designation select item.DesignationCode);
        //        if (DesignationInfoes.Count() > 0)
        //        {
        //            GetDCode = DesignationInfoes.Count() + 1;
        //            GetDCode1 = "FSD" + GetDCode.ToString();
        //        }
        //        else
        //        {
        //            GetDCode1 = "FSD";
        //        }

        //        dg.DesignationCode = GetDCode1;
        //        dg.Grade = Grade;
        //        dg.GrossSalary = GrossSalary;
        //        dg.BasicSalary = BasicSalary;
        //        dg.HomeRent = HomeRent;
        //        dg.FoodAllowance = FoodAllowance;
        //        dg.MedicalAllowance = Medical;
        //        dg.Conveyance = Conveyance;

        //        dg.AddBy = Request.Cookies["UserSettings"]["User_Name"];
        //        dg.AddDate = DateTime.Today;

        //        db.HR_Designation.Add(dg);
        //        db.SaveChanges();

        //        var jsonData = new { success = true, message = "Designation Added Successfully" };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //}


        //public ActionResult UpdateDesignation(HR_Designation dg, int Id, string Designation, string DesignationCode, string Grade, decimal GrossSalary, decimal BasicSalary, decimal HomeRent, decimal FoodAllowance, decimal Medical, decimal Conveyance)
        //{
        //    dg = db.HR_Designation.FirstOrDefault(x => x.DesignationId == Id);
        //    if (dg == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    dg.Designation = Designation;
        //    //dg.DesignationCode = DesignationCode;
        //    dg.Grade = Grade;
        //    dg.GrossSalary = GrossSalary;
        //    dg.BasicSalary = BasicSalary;
        //    dg.HomeRent = HomeRent;
        //    dg.FoodAllowance = FoodAllowance;
        //    dg.MedicalAllowance = Medical;
        //    dg.Conveyance = Conveyance;
        //    dg.EditBy = Request.Cookies["UserSettings"]["User_Name"];
        //    dg.EditDate = DateTime.Today;
        //    db.SaveChanges();

        //    return PartialView(db.HR_Designation.ToList());
        //}
        //// End Designation All Methods

        //public JsonResult DeleteDesignation(HR_Designation dg, int DesignationId, string DesignationCode)
        //{

        //    var checkAttend = (from item1 in db.HR_EmpBasicInformation where item1.DesignationCode == DesignationCode select item1).ToList();
        //    if (checkAttend.Count > 0)
        //    {
        //        // show message
        //        var jsonData = new { success = true, message = "Could not delete this designation. already use in." };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        dg = db.HR_Designation.FirstOrDefault(x => x.DesignationId == DesignationId);
        //        db.HR_Designation.Remove(dg);
        //        db.SaveChanges();
        //        var jsonData = new { success = true, message = "Designation Deleted Successfully" };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }

        //}

        //// Start Employee Type All Methods
        //public ActionResult EmployeeType()
        //{
        //    if (Request.Cookies["UserSettings"] != null)
        //    {
        //        if (Request.Cookies["UserSettings"]["User_Name"] != null)
        //        {
        //            User_Name = Request.Cookies["UserSettings"]["User_Name"];
        //        }
        //        return View(db.HR_EmployeeType.ToList());
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //}

        //public ActionResult EmployeeTypeList()
        //{
        //    return PartialView(db.HR_EmployeeType.ToList());
        //}

        //public ActionResult AddEmployeeType(HR_EmployeeType emp, string EmployeeType)
        //{
        //    emp.EmployeeType = EmployeeType;
        //    db.HR_EmployeeType.Add(emp);
        //    db.SaveChanges();
        //    return PartialView();
        //}

        //public ActionResult UpdateEmployeeType(HR_EmployeeType emp, int Id, string EmployeeType)
        //{
        //    emp = db.HR_EmployeeType.FirstOrDefault(x => x.EmpTypeId == Id);
        //    if (emp == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    emp.EmployeeType = EmployeeType;
        //    db.SaveChanges();
        //    return PartialView();

        //}
        //public ActionResult DeleteEmpType(HR_EmployeeType emp, int Id)
        //{
        //    emp = db.HR_EmployeeType.FirstOrDefault(x => x.EmpTypeId == Id);
        //    if (emp == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.HR_EmployeeType.Remove(emp);
        //    db.SaveChanges();
        //    return PartialView();

        //}
        //// End Employee Type All Methods




        //// Start Employee Category All Methods
        //public ActionResult EmployeeCategory()
        //{
        //    if (Request.Cookies["UserSettings"] != null)
        //    {
        //        if (Request.Cookies["UserSettings"]["User_Name"] != null)
        //        {
        //            User_Name = Request.Cookies["UserSettings"]["User_Name"];
        //        }
        //        return View(db.HR_EmployeeCategory.ToList());
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //}
        //public ActionResult EmpCatList()
        //{
        //    return PartialView(db.HR_EmployeeCategory.ToList());
        //}

        //public JsonResult AddEmpCat(HR_EmployeeCategory emp, string EmployeeGrade, string EmployeeCategory)
        //{
        //    emp.EmployeeGrade = EmployeeGrade;
        //    emp.EmployeeCategory = EmployeeCategory;
        //    emp.AddBy = Request.Cookies["UserSettings"]["User_Name"];
        //    emp.AddDate = DateTime.Today;
        //    db.HR_EmployeeCategory.Add(emp);
        //    db.SaveChanges();
        //    var jsonData = new { success = true, message = "Employee Category Created Successfully." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult UpdateEmpCat(HR_EmployeeCategory emp, int Id, string EmployeeGrade, string EmployeeCategory)
        //{
        //    emp = db.HR_EmployeeCategory.FirstOrDefault(x => x.EmpCategoryId == Id);
        //    emp.EmployeeGrade = EmployeeGrade;
        //    emp.EmployeeCategory = EmployeeCategory;
        //    emp.EditBy = Request.Cookies["UserSettings"]["User_Name"];
        //    emp.EditDate = DateTime.Today;
        //    db.SaveChanges();
        //    var jsonData = new { success = true, message = "Employee Category Updated Successfully." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);

        //}
        //public JsonResult DeleteEmpCat(HR_EmployeeCategory emp, int Id)
        //{
        //    emp = db.HR_EmployeeCategory.FirstOrDefault(x => x.EmpCategoryId == Id);
        //    db.HR_EmployeeCategory.Remove(emp);
        //    db.SaveChanges();
        //    var jsonData = new { success = true, message = "Employee Category Deleted Successfully." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);

        //}


        //// End Employee Category All Methods

        //// Start Education Type All Methods
        //public ActionResult EducationType()
        //{
        //    if (Request.Cookies["UserSettings"] != null)
        //    {
        //        if (Request.Cookies["UserSettings"]["User_Name"] != null)
        //        {
        //            User_Name = Request.Cookies["UserSettings"]["User_Name"];
        //        }
        //        return View(db.HR_EducationType.ToList());
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //}

        //public ActionResult EduTypeList()
        //{
        //    return PartialView(db.HR_EducationType.ToList());
        //}

        //public ActionResult AddEduType(HR_EducationType edu, string EducationType)
        //{
        //    edu.EducationType = EducationType;
        //    db.HR_EducationType.Add(edu);
        //    db.SaveChanges();
        //    return PartialView();
        //}

        //public ActionResult UpdateEduType(HR_EducationType edu, int Id, string EducationType)
        //{
        //    edu = db.HR_EducationType.FirstOrDefault(x => x.EduType_Id == Id);
        //    if (edu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    edu.EducationType = EducationType;
        //    db.SaveChanges();
        //    return PartialView();

        //}
        //public ActionResult DeleteEduType(HR_EducationType edu, int Id)
        //{
        //    edu = db.HR_EducationType.FirstOrDefault(x => x.EduType_Id == Id);
        //    if (edu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.HR_EducationType.Remove(edu);
        //    db.SaveChanges();
        //    return PartialView();

        //}
        // End Education Type All Methods

        // Start Holiday Type All Methods
        public ActionResult HolidayType()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["User_Name"] != null)
                {
                    User_Name = Request.Cookies["UserSettings"]["User_Name"];
                }
                return View(db.HR_HolidayType.ToList());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        public ActionResult HoliTypeList()
        {
            return PartialView(db.HR_HolidayType.ToList());
        }

        public ActionResult AddHoliType(HR_HolidayType holi, string HolidayType, string HolidayTypeCode)
        {
            holi.HolidayType = HolidayType;
            holi.HolidayTypeCode = HolidayTypeCode;
            db.HR_HolidayType.Add(holi);
            db.SaveChanges();
            return PartialView();
        }

        public ActionResult UpdateHoliType(HR_HolidayType holi, int Id, string HolidayType, string HolidayTypeCode)
        {
            holi = db.HR_HolidayType.FirstOrDefault(x => x.HoildayTypeId == Id);
            holi.HolidayType = HolidayType;
            holi.HolidayTypeCode = HolidayTypeCode;
            db.SaveChanges();
            return PartialView();

        }
        public ActionResult DeleteHoliType(HR_HolidayType holi, int Id)
        {
            holi = db.HR_HolidayType.FirstOrDefault(x => x.HoildayTypeId == Id);
            if (holi == null)
            {
                return HttpNotFound();
            }
            db.HR_HolidayType.Remove(holi);
            db.SaveChanges();
            return PartialView();

        }
        // End Holiday Type All Methods


        // Start Holiday Type All Methods
        public ActionResult HolidaySetup()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["User_Name"] != null)
                {
                    User_Name = Request.Cookies["UserSettings"]["User_Name"];
                }
                ViewBag.HolidayTypeCode = new SelectList(db.HR_HolidayType, "HolidayTypeCode", "HolidayType");
                return View(db.HR_Holiday.ToList());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult HolidaySetupList()
        {
            var Info = (from item in db.HR_Holiday group item by item.ToDate).ToList();
            return PartialView(Info);
        }

        public JsonResult AddHolidaySetup(HR_Holiday holi, string HolidayName, string HolidayTypeCode, DateTime FromDate, DateTime ToDate, float TotalDay)
        {
            for (int i = 0; i < TotalDay; i++)
            {
                holi.HolidayName = HolidayName;
                holi.HolidayTypeCode = HolidayTypeCode;
                holi.Holiday_Date = FromDate;
                holi.FromDate = FromDate.AddDays(i);
                holi.ToDate = Convert.ToDateTime(ToDate.ToString("yyyy-MM-dd"));
                holi.TotalDay = TotalDay;
                db.HR_Holiday.Add(holi);
                db.SaveChanges();
            }
            var jsonData = new { success = true, message = "Holiday Added Successfully" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateHolidaySetup(HR_Holiday holi, int Id, string HolidayName, string HolidayType, DateTime FromDate, DateTime ToDate, float TotalDay)
        {
            holi = db.HR_Holiday.FirstOrDefault(x => x.HoilidayId == Id);
            if (holi == null)
            {
                return HttpNotFound();
            }
            holi.HolidayName = HolidayName;
            holi.HolidayTypeCode = HolidayType;
            holi.FromDate = FromDate;
            holi.ToDate = ToDate;
            holi.TotalDay = TotalDay;
            db.SaveChanges();
            return PartialView();

        }

        public string checkStatus;
        public ActionResult DeleteHolidaySetup(HR_Holiday holi, HR_Holiday holi2, DateTime ToDate)
        {
            holi = db.HR_Holiday.FirstOrDefault(x => x.ToDate == ToDate);
            var checkAttend = (from item1 in db.HR_Attendance where item1.Attendance_Date == holi.FromDate select item1).FirstOrDefault();
            if (checkAttend.Attendance_Date != null)
            {
                // show message
                var jsonData = new { success = true, message = "Already exists this day in attendance table. Could not delete." };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Info2 = (from item in db.HR_Holiday where item.ToDate == holi.ToDate select item).ToList();
                if (Info2.Count > 0)
                {
                    foreach (var it in Info2)
                    {
                        holi2 = db.HR_Holiday.FirstOrDefault(x => x.HoilidayId == it.HoilidayId);
                        db.HR_Holiday.Remove(holi2);
                    }
                    db.SaveChanges();
                }
                var jsonData = new { success = true, message = "Delete Successfully." };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }
        // End Holiday Type All Methods

        public ActionResult LeaveType()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                if (Request.Cookies["UserSettings"]["User_Name"] != null)
                {
                    User_Name = Request.Cookies["UserSettings"]["User_Name"];
                }
                return View(db.HR_LeaveType.ToList());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult LeaveTypeList()
        {
            return PartialView(db.HR_LeaveType.ToList());
        }

        public ActionResult AddLeaveType(HR_LeaveType leave, string LeaveType, string LeaveTypeCode, float LeaveDay)
        {
            leave.LeaveType = LeaveType;
            leave.LeaveTypeCode = LeaveTypeCode;
            leave.LeaveDay = LeaveDay;
            db.HR_LeaveType.Add(leave);
            db.SaveChanges();
            return PartialView();
        }

        public ActionResult UpdateLeaveType(HR_LeaveType leave, int Id, string LeaveType, string LeaveTypeCode, float LeaveDay)
        {
            leave = db.HR_LeaveType.FirstOrDefault(x => x.LeaveTypeId == Id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            leave.LeaveType = LeaveType;
            leave.LeaveTypeCode = LeaveTypeCode;
            leave.LeaveDay = LeaveDay;
            db.SaveChanges();
            return PartialView();

        }

        //public ActionResult SalaryMatrix()
        //{
        //    var at = db.HR_SalaryMatrix.FirstOrDefault();
        //    ViewBag.Basic = at.BasicSalary_Percent;
        //    ViewBag.Home_Rent = at.HomeRent_Percent;
        //    ViewBag.Medical = at.MedicalAllowance_Percent;
        //    ViewBag.Conveyance = at.Conveyance_Percent;

        //    return View(db.HR_SalaryMatrix.ToList());
        //}

        //[HttpPost]
        //public JsonResult SalaryMatrix(HR_SalaryMatrix sm, int SalaryMatrixId, double BasicSalary_Percent, double HomeRent_Percent, double MedicalAllowance_Percent, double Conveyance_Percent)
        //{
        //    var Info = (from item in db.HR_SalaryMatrix where item.SalaryMatrixId == SalaryMatrixId select item).ToList();
        //    if (Info.Count > 0)
        //    {
        //        sm = db.HR_SalaryMatrix.FirstOrDefault(x => x.SalaryMatrixId == SalaryMatrixId);
        //        sm.BasicSalary_Percent = BasicSalary_Percent;
        //        sm.HomeRent_Percent = HomeRent_Percent;
        //        sm.MedicalAllowance_Percent = MedicalAllowance_Percent;
        //        sm.Conveyance_Percent = Conveyance_Percent;
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        sm.BasicSalary_Percent = BasicSalary_Percent;
        //        sm.HomeRent_Percent = HomeRent_Percent;
        //        sm.MedicalAllowance_Percent = MedicalAllowance_Percent;
        //        sm.Conveyance_Percent = Conveyance_Percent;
        //        db.HR_SalaryMatrix.Add(sm);
        //        db.SaveChanges();
        //    }

        //    var jsonData = new { success = true, message = "Salary Matrix Updated Successfully." };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


	}
}