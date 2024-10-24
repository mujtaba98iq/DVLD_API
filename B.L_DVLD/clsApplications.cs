using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace ApplicationsBusinessLayer
{

public class clsApplications
{
 public ApplicationsDTO ApplicationDTO
        { get { return (new ApplicationsDTO(this.ApplicationID,this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID)); }}
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


public clsApplications()
{
this.ApplicationID = default;
this.ApplicantPersonID = default;
this.ApplicationDate = default;
this.ApplicationTypeID = default;
this.ApplicationStatus = default;
this.LastStatusDate = default;
this.PaidFees = default;
this.CreatedByUserID = default;


Mode = enMode.AddNew;

}

public clsApplications(ApplicationsDTO ADTO, enMode cMode =enMode.AddNew) 
{
this.ApplicationID = ADTO.ApplicationID;
this.ApplicantPersonID = ADTO.ApplicantPersonID;
this.ApplicationDate = ADTO.ApplicationDate;
this.ApplicationTypeID = ADTO.ApplicationTypeID;
this.ApplicationStatus = ADTO.ApplicationStatus;
this.LastStatusDate = ADTO.LastStatusDate;
this.PaidFees = ADTO.PaidFees;
this.CreatedByUserID = ADTO.CreatedByUserID;


Mode = cMode;

}

private bool _AddNewApplications()
{
this.ApplicationID = clsApplicationsDataAccess.AddApplications(ApplicationDTO);
return (this.ApplicationID != -1);

}

private bool _UpdateApplications()
{
return clsApplicationsDataAccess.UpdateApplications(ApplicationDTO);
}

public static clsApplications Find(int Id)
{
ApplicationsDTO ADTO = clsApplicationsDataAccess.GetApplicationsInfoByID(Id);

if(ADTO != null)
return new clsApplications(ADTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplications())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplications();

            }




            return false;
        }

public static List<ApplicationsDTO> GetAllApplications(){return clsApplicationsDataAccess.GetAllApplications();}

public static bool DeleteApplications(int ApplicationID){return  clsApplicationsDataAccess.DeleteApplications(ApplicationID);}

public static bool isApplicationsExist(int ApplicationID){return clsApplicationsDataAccess.IsApplicationsExist(ApplicationID);}


}

}