using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace LocalDrivingLicenseFullApplications_ViewBusinessLayer
{

public class clsLocalDrivingLicenseFullApplications_View
{
 public LocalDrivingLicenseFullApplications_ViewDTO LDTO
  {get { return (new LocalDrivingLicenseFullApplications_ViewDTO(this.ApplicationID,this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID,this.LocalDrivingLicenseApplicationID,this.LicenseClassID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int ApplicationID {get; set;}
public int ApplicantPersonID {get; set;}
public DateTime ApplicationDate {get; set;}
public int ApplicationTypeID {get; set;}
public byte ApplicationStatus {get; set;}
public DateTime LastStatusDate {get; set;}
public decimal PaidFees {get; set;}
public int CreatedByUserID {get; set;}
public int LocalDrivingLicenseApplicationID {get; set;}
public int LicenseClassID {get; set;}


public clsLocalDrivingLicenseFullApplications_View()
{
this.ApplicationID = default;
this.ApplicantPersonID = default;
this.ApplicationDate = default;
this.ApplicationTypeID = default;
this.ApplicationStatus = default;
this.LastStatusDate = default;
this.PaidFees = default;
this.CreatedByUserID = default;
this.LocalDrivingLicenseApplicationID = default;
this.LicenseClassID = default;


Mode = enMode.AddNew;

}

public clsLocalDrivingLicenseFullApplications_View(LocalDrivingLicenseFullApplications_ViewDTO LDTO, enMode cMode =enMode.AddNew) 
{
this.ApplicationID = LDTO.ApplicationID;
this.ApplicantPersonID = LDTO.ApplicantPersonID;
this.ApplicationDate = LDTO.ApplicationDate;
this.ApplicationTypeID = LDTO.ApplicationTypeID;
this.ApplicationStatus = LDTO.ApplicationStatus;
this.LastStatusDate = LDTO.LastStatusDate;
this.PaidFees = LDTO.PaidFees;
this.CreatedByUserID = LDTO.CreatedByUserID;
this.LocalDrivingLicenseApplicationID = LDTO.LocalDrivingLicenseApplicationID;
this.LicenseClassID = LDTO.LicenseClassID;


Mode = cMode;

}

private bool _AddNewLocalDrivingLicenseFullApplications_View()
{
this.ApplicationID = clsLocalDrivingLicenseFullApplications_ViewDataAccess.AddLocalDrivingLicenseFullApplications_View(LDTO);
return (this.ApplicationID != -1);

}

private bool _UpdateLocalDrivingLicenseFullApplications_View()
{
return clsLocalDrivingLicenseFullApplications_ViewDataAccess.UpdateLocalDrivingLicenseFullApplications_View(LDTO);
}

public static clsLocalDrivingLicenseFullApplications_View Find(int Id)
{
LocalDrivingLicenseFullApplications_ViewDTO LDTO = clsLocalDrivingLicenseFullApplications_ViewDataAccess.GetLocalDrivingLicenseFullApplications_ViewInfoByID(Id);

if(LDTO != null)
return new clsLocalDrivingLicenseFullApplications_View(LDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseFullApplications_View())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseFullApplications_View();

            }




            return false;
        }

public static List<LocalDrivingLicenseFullApplications_ViewDTO> GetAllLocalDrivingLicenseFullApplications_View(){return clsLocalDrivingLicenseFullApplications_ViewDataAccess.GetAllLocalDrivingLicenseFullApplications_View();}

public static bool DeleteLocalDrivingLicenseFullApplications_View(int ApplicationID){return  clsLocalDrivingLicenseFullApplications_ViewDataAccess.DeleteLocalDrivingLicenseFullApplications_View(ApplicationID);}

public static bool isApplicationExist(int ApplicationID){return clsLocalDrivingLicenseFullApplications_ViewDataAccess.IsLocalDrivingLicenseFullApplications_ViewExist(ApplicationID);}


}

}