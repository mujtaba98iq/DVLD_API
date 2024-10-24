using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace InternationalLicensesBusinessLayer
{

public class clsInternationalLicenses
{
 public InternationalLicensesDTO InternationalLicensDTO
        { get { return (new InternationalLicensesDTO(this.InternationalAppID,this.ApplicatoinID,this.DirverID,this.LocalDriverApplicationID,this.IssueDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int InternationalAppID {get; set;}
public int ApplicatoinID {get; set;}
public int DirverID {get; set;}
public int LocalDriverApplicationID {get; set;}
public DateTime IssueDate {get; set;}
public DateTime ExpirationDate {get; set;}
public bool IsActive {get; set;}
public int CreatedByUserID {get; set;}


public clsInternationalLicenses()
{
this.InternationalAppID = default;
this.ApplicatoinID = default;
this.DirverID = default;
this.LocalDriverApplicationID = default;
this.IssueDate = default;
this.ExpirationDate = default;
this.IsActive = default;
this.CreatedByUserID = default;


Mode = enMode.AddNew;

}

public clsInternationalLicenses(InternationalLicensesDTO IDTO, enMode cMode =enMode.AddNew) 
{
this.InternationalAppID = IDTO.InternationalAppID;
this.ApplicatoinID = IDTO.ApplicatoinID;
this.DirverID = IDTO.DirverID;
this.LocalDriverApplicationID = IDTO.LocalDriverApplicationID;
this.IssueDate = IDTO.IssueDate;
this.ExpirationDate = IDTO.ExpirationDate;
this.IsActive = IDTO.IsActive;
this.CreatedByUserID = IDTO.CreatedByUserID;


Mode = cMode;

}

private bool _AddNewInternationalLicenses()
{
this.InternationalAppID = clsInternationalLicensesDataAccess.AddInternationalLicenses(InternationalLicensDTO);
return (this.InternationalAppID != -1);

}

private bool _UpdateInternationalLicenses()
{
return clsInternationalLicensesDataAccess.UpdateInternationalLicenses(InternationalLicensDTO);
}

public static clsInternationalLicenses Find(int Id)
{
InternationalLicensesDTO IDTO = clsInternationalLicensesDataAccess.GetInternationalLicensesInfoByID(Id);

if(IDTO != null)
return new clsInternationalLicenses(IDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicenses();

            }




            return false;
        }

public static List<InternationalLicensesDTO> GetAllInternationalLicenses(){return clsInternationalLicensesDataAccess.GetAllInternationalLicenses();}

public static bool DeleteInternationalLicenses(int InternationalAppID){return  clsInternationalLicensesDataAccess.DeleteInternationalLicenses(InternationalAppID);}

public static bool isInternationalAppExist(int InternationalAppID){return clsInternationalLicensesDataAccess.IsInternationalLicensesExist(InternationalAppID);}


}

}