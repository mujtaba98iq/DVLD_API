using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace TestAppointmentBusinessLayer
{

public class clsTestAppointment
{
        int _RetakeTestApplicationID = -1;
 public TestAppointmentDTO TestAppointmentDTO
        { get { return (new TestAppointmentDTO(this.TestAppointmentID,this.TestTypeID,this.LocalDrivingLicenseApplicationID,this.AppointmentDate,this.PaidFees,this.CreatedByUserID,this.IsLocked,this._RetakeTestApplicationID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int TestAppointmentID {get; set;}
public int TestTypeID {get; set;}
public int LocalDrivingLicenseApplicationID {get; set;}
public DateTime AppointmentDate {get; set;}
public decimal PaidFees {get; set;}
public int CreatedByUserID {get; set;}
public bool IsLocked {get; set;}
public int? RetakeTestApplicationID {get; set;}


        public clsTestAppointment()
        {
            // تعيين القيم الافتراضية
            TestAppointmentID = 0; // أو -1 إذا كان منطقياً
            TestTypeID = 0; // يمكن تخصيص القيمة الافتراضية حسب الحاجة
            LocalDrivingLicenseApplicationID = 0;
            AppointmentDate = DateTime.MinValue; // قيمة افتراضية لتاريخ
            PaidFees = 0.0m; // قيمة افتراضية للمدفوعات
            CreatedByUserID = 0; // أو -1 حسب المنطق
            IsLocked = false; // قيمة افتراضية لـ bool
            RetakeTestApplicationID = null; // يمكن أن تكون null
            Mode = enMode.AddNew; // تعيين الحالة الحالية
        }

        public clsTestAppointment(TestAppointmentDTO TDTO, enMode cMode = enMode.AddNew)
        {
            this.TestAppointmentID = TDTO.TestAppointmentID;
            this.TestTypeID = TDTO.TestTypeID;
            this.LocalDrivingLicenseApplicationID = TDTO.LocalDrivingLicenseApplicationID;
            this.AppointmentDate = TDTO.AppointmentDate;
            this.PaidFees = TDTO.PaidFees;
            this.CreatedByUserID = TDTO.CreatedByUserID;
            this.IsLocked = TDTO.IsLocked;

            // تعيين القيمة مباشرة
            this._RetakeTestApplicationID = TDTO.RetakeTestApplicationID ?? default;

            Mode = cMode;
        }


        private bool _AddNewTestAppointment()
{
this.TestAppointmentID = clsTestAppointmentDataAccess.AddTestAppointment(TestAppointmentDTO);
return (this.TestAppointmentID != -1);

}

private bool _UpdateTestAppointment()
{
return clsTestAppointmentDataAccess.UpdateTestAppointment(TestAppointmentDTO);
}

public static clsTestAppointment Find(int Id)
{
TestAppointmentDTO TDTO = clsTestAppointmentDataAccess.GetTestAppointmentInfoByID(Id);

if(TDTO != null)
return new clsTestAppointment(TDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }




            return false;
        }

public static List<TestAppointmentDTO> GetAllTestAppointment(){return clsTestAppointmentDataAccess.GetAllTestAppointment();}

public static bool DeleteTestAppointment(int TestAppointmentID){return  clsTestAppointmentDataAccess.DeleteTestAppointment(TestAppointmentID);}

public static async Task<bool> isTestAppointmentExist(int TestAppointmentID){return await clsTestAppointmentDataAccess.IsTestAppointmentExistAsync(TestAppointmentID);}


}

}