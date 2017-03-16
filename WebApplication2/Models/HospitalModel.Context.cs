﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitalManagementSystemDBEntities : DbContext
    {
        public HospitalManagementSystemDBEntities()
            : base("name=HospitalManagementSystemDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<HR_AdvanceSalaryDetailPrevious_Log> HR_AdvanceSalaryDetailPrevious_Log { get; set; }
        public virtual DbSet<HR_AdvanceSalaryDetails> HR_AdvanceSalaryDetails { get; set; }
        public virtual DbSet<HR_AdvanceSalarySummary> HR_AdvanceSalarySummary { get; set; }
        public virtual DbSet<HR_Attendance> HR_Attendance { get; set; }
        public virtual DbSet<HR_Attendance_Manual_Log> HR_Attendance_Manual_Log { get; set; }
        public virtual DbSet<HR_AttendanceBonus> HR_AttendanceBonus { get; set; }
        public virtual DbSet<HR_AttendanceProcess_Log> HR_AttendanceProcess_Log { get; set; }
        public virtual DbSet<HR_AttendanceStatusProcess_Log> HR_AttendanceStatusProcess_Log { get; set; }
        public virtual DbSet<HR_Holiday> HR_Holiday { get; set; }
        public virtual DbSet<HR_HolidayType> HR_HolidayType { get; set; }
        public virtual DbSet<HR_HospitalSetup> HR_HospitalSetup { get; set; }
        public virtual DbSet<HR_LeaveApply> HR_LeaveApply { get; set; }
        public virtual DbSet<HR_LeaveType> HR_LeaveType { get; set; }
        public virtual DbSet<HR_Salary_Deduction> HR_Salary_Deduction { get; set; }
        public virtual DbSet<HR_Salary_Punishment> HR_Salary_Punishment { get; set; }
        public virtual DbSet<HR_Shift> HR_Shift { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbe_NurseSectionType> tbe_NurseSectionType { get; set; }
        public virtual DbSet<tbl_Advance> tbl_Advance { get; set; }
        public virtual DbSet<tbl_AllowanceDetails> tbl_AllowanceDetails { get; set; }
        public virtual DbSet<tbl_AllowanceType> tbl_AllowanceType { get; set; }
        public virtual DbSet<tbl_AllowanceVoucher> tbl_AllowanceVoucher { get; set; }
        public virtual DbSet<tbl_AmbCatagory> tbl_AmbCatagory { get; set; }
        public virtual DbSet<tbl_AmbulanceInfo> tbl_AmbulanceInfo { get; set; }
        public virtual DbSet<tbl_AmbulanceSchedule> tbl_AmbulanceSchedule { get; set; }
        public virtual DbSet<tbl_AppointmentSchedule> tbl_AppointmentSchedule { get; set; }
        public virtual DbSet<tbl_Attendance> tbl_Attendance { get; set; }
        public virtual DbSet<tbl_BloodDonerInfo> tbl_BloodDonerInfo { get; set; }
        public virtual DbSet<tbl_BloodGroup> tbl_BloodGroup { get; set; }
        public virtual DbSet<tbl_BloodPurchaseeDetails> tbl_BloodPurchaseeDetails { get; set; }
        public virtual DbSet<tbl_BloodPurchaseVoucher> tbl_BloodPurchaseVoucher { get; set; }
        public virtual DbSet<tbl_BloodSupplierInfo> tbl_BloodSupplierInfo { get; set; }
        public virtual DbSet<tbl_BloodSupplierProfile> tbl_BloodSupplierProfile { get; set; }
        public virtual DbSet<tbl_Claim> tbl_Claim { get; set; }
        public virtual DbSet<tbl_DesignationType> tbl_DesignationType { get; set; }
        public virtual DbSet<tbl_DoctorDuty> tbl_DoctorDuty { get; set; }
        public virtual DbSet<tbl_DoctorSpecialization> tbl_DoctorSpecialization { get; set; }
        public virtual DbSet<tbl_IndorBloodSalesDetails> tbl_IndorBloodSalesDetails { get; set; }
        public virtual DbSet<tbl_IndorBloodSalesVoucher> tbl_IndorBloodSalesVoucher { get; set; }
        public virtual DbSet<tbl_IndorMedicenPurshaseVoucher> tbl_IndorMedicenPurshaseVoucher { get; set; }
        public virtual DbSet<tbl_IndorMedicinePurchaseDetails> tbl_IndorMedicinePurchaseDetails { get; set; }
        public virtual DbSet<tbl_IndorMedicineSalesDetails> tbl_IndorMedicineSalesDetails { get; set; }
        public virtual DbSet<tbl_IndorMedicineSalesVoucher> tbl_IndorMedicineSalesVoucher { get; set; }
        public virtual DbSet<Tbl_IndorPatientLabTest> Tbl_IndorPatientLabTest { get; set; }
        public virtual DbSet<tbl_InvoiceDetails> tbl_InvoiceDetails { get; set; }
        public virtual DbSet<tbl_InvoiceVocher> tbl_InvoiceVocher { get; set; }
        public virtual DbSet<tbl_LabInfo> tbl_LabInfo { get; set; }
        public virtual DbSet<tbl_Leave> tbl_Leave { get; set; }
        public virtual DbSet<tbl_Loan> tbl_Loan { get; set; }
        public virtual DbSet<tbl_MedicinCatagory> tbl_MedicinCatagory { get; set; }
        public virtual DbSet<tbl_MedicineInfo> tbl_MedicineInfo { get; set; }
        public virtual DbSet<tbl_MedicineSupplierInfo> tbl_MedicineSupplierInfo { get; set; }
        public virtual DbSet<tbl_MedicineSupplierProfile> tbl_MedicineSupplierProfile { get; set; }
        public virtual DbSet<tbl_MedicinUnit> tbl_MedicinUnit { get; set; }
        public virtual DbSet<tbl_NurseDuty> tbl_NurseDuty { get; set; }
        public virtual DbSet<tbl_OperationInfo> tbl_OperationInfo { get; set; }
        public virtual DbSet<tbl_OutdorBloodSalesDetails> tbl_OutdorBloodSalesDetails { get; set; }
        public virtual DbSet<tbl_OutdorBloodSalesVoucher> tbl_OutdorBloodSalesVoucher { get; set; }
        public virtual DbSet<tbl_OutdorMedicineSalesDetails> tbl_OutdorMedicineSalesDetails { get; set; }
        public virtual DbSet<tbl_OutdorMedicineSalesVoucher> tbl_OutdorMedicineSalesVoucher { get; set; }
        public virtual DbSet<tbl_OutdorPatientLabTest> tbl_OutdorPatientLabTest { get; set; }
        public virtual DbSet<tbl_PatientInfo> tbl_PatientInfo { get; set; }
        public virtual DbSet<tbl_PationRoomInformation> tbl_PationRoomInformation { get; set; }
        public virtual DbSet<tbl_Referenceinfo> tbl_Referenceinfo { get; set; }
        public virtual DbSet<tbl_RoomCatagory> tbl_RoomCatagory { get; set; }
        public virtual DbSet<tbl_RoomType> tbl_RoomType { get; set; }
        public virtual DbSet<tbl_Salary> tbl_Salary { get; set; }
        public virtual DbSet<tbl_Service> tbl_Service { get; set; }
        public virtual DbSet<tbl_SpecializationType> tbl_SpecializationType { get; set; }
        public virtual DbSet<tbl_StaffEducation> tbl_StaffEducation { get; set; }
        public virtual DbSet<tbl_staffInfo> tbl_staffInfo { get; set; }
        public virtual DbSet<tbl_StaffType> tbl_StaffType { get; set; }
        public virtual DbSet<tbl_UserInfo> tbl_UserInfo { get; set; }
        public virtual DbSet<tbl_UserRole> tbl_UserRole { get; set; }
    }
}
