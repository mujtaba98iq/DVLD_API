using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace LicensesBusinessLayer
{

public class clsLicenses
{
 public LicensesDTO LicensDTO
        { get { return (new LicensesDTO(this.LicenseID,this.ApplicationID,this.DriverID,this.LicenseClass,this.IssueDate,this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,this.IssueReason,this.CreatedByUserID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int LicenseID {get; set;}
public int ApplicationID {get; set;}
public int DriverID {get; set;}
public int LicenseClass {get; set;}
public DateTime IssueDate {get; set;}
public DateTime ExpirationDate {get; set;}
public string Notes {get; set;}
public decimal PaidFees {get; set;}
public bool IsActive {get; set;}
public byte IssueReason {get; set;}
public int CreatedByUserID {get; set;}


public clsLicenses()
{
this.LicenseID = default;
this.ApplicationID = default;
this.DriverID = default;
this.LicenseClass = default;
this.IssueDate = default;
this.ExpirationDate = default;
this.Notes = default;
this.PaidFees = default;
this.IsActive = default;
this.IssueReason = default;
this.CreatedByUserID = default;


Mode = enMode.AddNew;

}

public clsLicenses(LicensesDTO LDTO, enMode cMode =enMode.AddNew) 
{
this.LicenseID = LDTO.LicenseID;
this.ApplicationID = LDTO.ApplicationID;
this.DriverID = LDTO.DriverID;
this.LicenseClass = LDTO.LicenseClass;
this.IssueDate = LDTO.IssueDate;
this.ExpirationDate = LDTO.ExpirationDate;
this.Notes = LDTO.Notes;
this.PaidFees = LDTO.PaidFees;
this.IsActive = LDTO.IsActive;
this.IssueReason = LDTO.IssueReason;
this.CreatedByUserID = LDTO.CreatedByUserID;


Mode = cMode;

}

private bool _AddNewLicenses()
{
this.LicenseID = clsLicensesDataAccess.AddLicenses(LicensDTO);
return (this.LicenseID != -1);

}

private bool _UpdateLicenses()
{
return clsLicensesDataAccess.UpdateLicenses(LicensDTO);
}

public static clsLicenses Find(int Id)
{
LicensesDTO LDTO = clsLicensesDataAccess.GetLicensesInfoByID(Id);

if(LDTO != null)
return new clsLicenses(LDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenses();

            }




            return false;
        }

public static List<LicensesDTO> GetAllLicenses(){return clsLicensesDataAccess.GetAllLicenses();}

public static bool DeleteLicenses(int LicenseID){return  clsLicensesDataAccess.DeleteLicenses(LicenseID);}

public static bool isLicenseExist(int LicenseID){return clsLicensesDataAccess.IsLicensesExist(LicenseID);}


}

}