using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace LocalDrivingLicenseApplicationsBusinessLayer
{

public class clsLocalDrivingLicenseApplications
{
 public LocalDrivingLicenseApplicationsDTO LocalDrivingLicenseApplicationDTO
        { get { return (new LocalDrivingLicenseApplicationsDTO(this.LocalLicenseApplicationID,this.ApplicationID,this.LicenseClassID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int LocalLicenseApplicationID {get; set;}
public int ApplicationID {get; set;}
public int LicenseClassID {get; set;}


public clsLocalDrivingLicenseApplications()
{
this.LocalLicenseApplicationID = default;
this.ApplicationID = default;
this.LicenseClassID = default;


Mode = enMode.AddNew;

}

public clsLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationsDTO LDTO, enMode cMode =enMode.AddNew) 
{
this.LocalLicenseApplicationID = LDTO.LocalLicenseApplicationID;
this.ApplicationID = LDTO.ApplicationID;
this.LicenseClassID = LDTO.LicenseClassID;


Mode = cMode;

}

private bool _AddNewLocalDrivingLicenseApplications()
{
this.LocalLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationDTO);
return (this.LocalLicenseApplicationID != -1);

}

private bool _UpdateLocalDrivingLicenseApplications()
{
return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationDTO);
}

public static clsLocalDrivingLicenseApplications Find(int Id)
{
LocalDrivingLicenseApplicationsDTO LDTO = clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationsInfoByID(Id);

if(LDTO != null)
return new clsLocalDrivingLicenseApplications(LDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplications();

            }




            return false;
        }

public static List<LocalDrivingLicenseApplicationsDTO> GetAllLocalDrivingLicenseApplications(){return clsLocalDrivingLicenseApplicationsDataAccess.GetAllLocalDrivingLicenseApplications();}

public static bool DeleteLocalDrivingLicenseApplications(int LocalLicenseApplicationID){return  clsLocalDrivingLicenseApplicationsDataAccess.DeleteLocalDrivingLicenseApplications(LocalLicenseApplicationID);}

public static bool isLocalDrivingLicenseApplicationsExist(int LocalLicenseApplicationID){return clsLocalDrivingLicenseApplicationsDataAccess.IsLocalDrivingLicenseApplicationsExist(LocalLicenseApplicationID);}


}

}