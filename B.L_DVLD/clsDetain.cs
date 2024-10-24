using System;
using System.Data;
using System.Runtime.CompilerServices;
using MyDVLDDataAccessLayer;
namespace DetainedLicensesBusinessLayer
{

public class clsDetainedLicenses
{
       
        
 public DetainedLicensesDTO DetainedLicensDTO
        {
            get { return (new DetainedLicensesDTO(this.DetainID,this.LicenseID,this.DetainDate,this.FineFees,this.CreatedByUserID,this.IsReleased,this.ReleaseDate,this.ReleasedByUserID , this.ReleaseApplicationID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int DetainID {get; set;}
public int LicenseID {get; set;}
public DateTime DetainDate {get; set;}
public decimal FineFees {get; set;}
public int CreatedByUserID {get; set;}
public bool IsReleased {get; set;}
public DateTime? ReleaseDate {get; set;}
public int? ReleasedByUserID {get; set;}
public int? ReleaseApplicationID {get; set;}

        


public clsDetainedLicenses()
{
this.DetainID = default;
this.LicenseID = default;
this.DetainDate = default;
this.FineFees = default;
this.CreatedByUserID = default;
this.IsReleased = default;
this.ReleaseDate = default;
this.ReleasedByUserID = default;
this.ReleaseApplicationID = default;
        

Mode = enMode.AddNew;

}

public clsDetainedLicenses(DetainedLicensesDTO DDTO, enMode cMode =enMode.AddNew) 
{
this.DetainID = DDTO.DetainID;
this.LicenseID = DDTO.LicenseID;
this.DetainDate = DDTO.DetainDate;
this.FineFees = DDTO.FineFees;
this.CreatedByUserID = DDTO.CreatedByUserID;
this.IsReleased = DDTO.IsReleased;
this.ReleaseDate = DDTO.ReleaseDate;
this.ReleasedByUserID = DDTO.ReleasedByUserID;
this.ReleaseApplicationID = DDTO.ReleaseApplicationID;


            Mode = cMode;

}

private bool _AddNewDetainedLicenses()
{
this.DetainID = clsDetainedLicensesDataAccess.AddDetainedLicenses(DetainedLicensDTO);
return (this.DetainID != -1);

}

private bool _UpdateDetainedLicenses()
{
return clsDetainedLicensesDataAccess.UpdateDetainedLicenses(DetainedLicensDTO);
}

public static clsDetainedLicenses Find(int Id)
{
DetainedLicensesDTO DDTO = clsDetainedLicensesDataAccess.GetDetainedLicensesInfoByID(Id);

if(DDTO != null)
return new clsDetainedLicenses(DDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicenses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicenses();

            }




            return false;
        }

public static List<DetainedLicensesDTO> GetAllDetainedLicenses(){return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();}

public static bool DeleteDetainedLicenses(int DetainID){return  clsDetainedLicensesDataAccess.DeleteDetainedLicenses(DetainID);}

public static async Task<bool>  isDetainExist(int DetainID){  return await clsDetainedLicensesDataAccess.IsDetainedLicensesExist(DetainID);}


}

}