using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace WebApplication2.Models
{
    //public class MyModel
    //{
    public partial class tbl_Advance
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Advance> AdvancedListResults { get; set; }
    }
    public partial class tbl_AllowanceVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_AllowanceVoucher> AllowanceListResults { get; set; }
    }
    public partial class tbl_AmbulanceInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_AmbulanceInfo> ambulanceInfoListResults { get; set; }
    }

    public partial class tbl_AmbulanceSchedule
    {
        public int? Page { get; set; }
        public IPagedList<tbl_AmbulanceSchedule> ambulanceScheduleListsResults { get; set; }
    }
    public partial class tbl_AppointmentSchedule
    {
        public int? Page { get; set; }
        public IPagedList<tbl_AppointmentSchedule> appointmentScheduleListResults { get; set; }
    }
    public partial class tbl_Attendance
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Attendance> AttendanceListResult { get; set; }
    }
    public partial class tbl_BloodDonerInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_BloodDonerInfo> AddDonerListResults { get; set; }
    }
    public partial class tbl_BloodPurchaseVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_BloodPurchaseVoucher> bloodpurchaseListResults { get; set; }
    }
    public partial class tbl_BloodSupplierInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_BloodSupplierInfo> bloodsupplierListResults { get; set; }
    }
    public partial class tbl_BloodSupplierProfile
    {
        public int? Page { get; set; }
        public IPagedList<tbl_BloodSupplierProfile> bloodSupplierProfileListResults { get; set; }
    }
    public partial class tbl_DoctorDuty
    {
        public int? Page { get; set; }
        public IPagedList<tbl_DoctorDuty> doctorDutyListResults { get; set; }
    }
    public partial class tbl_IndorBloodSalesVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_IndorBloodSalesVoucher> indorBloodsalesListsResults { get; set; }
    }
    public partial class tbl_IndorMedicenPurshaseVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_IndorMedicenPurshaseVoucher> IndorMedicinePurchaseListResults { get; set; }
    }

    public partial class tbl_IndorMedicineSalesVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_IndorMedicineSalesVoucher> IndormedicineSaleListResults { get; set; }
    }
    public partial class Tbl_IndorPatientLabTest
    {
        public int? Page { get; set; }
        public IPagedList<Tbl_IndorPatientLabTest> indorpatientlabtestListsResults { get; set; }
    }
    public partial class tbl_InvoiceVocher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_InvoiceVocher> InvoicelistResults { get; set; }
    }
    public partial class tbl_LabInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_LabInfo> labinfoListResults { get; set; }
    }
    public partial class tbl_Leave
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Leave> leaveListResults { get; set; }
    }
    public partial class tbl_Loan
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Loan> loanListResults { get; set; }
    }
    public partial class tbl_MedicineInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_MedicineInfo> medicineInfoListResults { get; set; }
    }
    public partial class tbl_MedicineSupplierInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_MedicineSupplierInfo> medicineSupplierListResults { get; set; }
    }
    public partial class tbl_MedicineSupplierProfile
    {
        public int? Page { get; set; }
        public IPagedList<tbl_MedicineSupplierProfile> MedicinesupplierProfileListResults { get; set; }
    }
    public partial class tbl_NurseDuty
    {
        public int? Page { get; set; }
        public IPagedList<tbl_NurseDuty> nurseDutyListResults { get; set; }
    }
    public partial class tbl_OutdorBloodSalesVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_OutdorBloodSalesVoucher> OutdoorBloodSalesListResults { get; set; }
    }
    public partial class tbl_OutdorMedicineSalesVoucher
    {
        public int? Page { get; set; }
        public IPagedList<tbl_OutdorMedicineSalesVoucher> outdorMedicineSalesListResults { get; set; }
    }
    public partial class tbl_OutdorPatientLabTest
    {
        public int? Page { get; set; }
        public IPagedList<tbl_OutdorPatientLabTest> outdorpatientlabtestListsResults { get; set; }
    }
    public partial class tbl_PatientInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_PatientInfo> patientInfoListResult { get; set; }
    }
    public partial class tbl_PationRoomInformation
    {
        public int? Page { get; set; }
        public IPagedList<tbl_PationRoomInformation> pationRoomInformationListResults { get; set; }
    }
    public partial class tbl_Referenceinfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Referenceinfo> referenceListResults { get; set; }
    }
    public partial class tbl_Salary
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Salary> SalaryListResults { get; set; }
    }
    public partial class tbl_staffInfo
    {
        public int? Page { get; set; }
        public IPagedList<tbl_staffInfo> staffListListsResults { get; set; }
    }
    public partial class tbl_Claim
    {
        public int? Page { get; set; }
        public IPagedList<tbl_Claim> claimListResult { get; set; }
    }
}
//}