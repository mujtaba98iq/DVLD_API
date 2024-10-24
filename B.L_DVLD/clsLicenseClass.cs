using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace LincnseClassesBusinessLayer
{

public class clsLincnseClasses
{

 public LincnseClassesDTO LincnseClassDTO
        { get { return (new LincnseClassesDTO(this.LicenseClassID,this.ClassName,this.ClassDescription,this.MinimumAllowedAge,this.DefaultValidityLength,this.ClassFees)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int LicenseClassID {get; set;}
public string ClassName {get; set;}
public string ClassDescription {get; set;}
public byte MinimumAllowedAge {get; set;}
public byte DefaultValidityLength {get; set;}
public decimal ClassFees {get; set;}


public clsLincnseClasses()
{
this.LicenseClassID = default;
this.ClassName = default;
this.ClassDescription = default;
this.MinimumAllowedAge = default;
this.DefaultValidityLength = default;
this.ClassFees = default;


Mode = enMode.AddNew;

}

public clsLincnseClasses(LincnseClassesDTO LDTO, enMode cMode =enMode.AddNew) 
{
this.LicenseClassID = LDTO.LicenseClassID;
this.ClassName = LDTO.ClassName;
this.ClassDescription = LDTO.ClassDescription;
this.MinimumAllowedAge = LDTO.MinimumAllowedAge;
this.DefaultValidityLength = LDTO.DefaultValidityLength;
this.ClassFees = LDTO.ClassFees;


Mode = cMode;

}

private bool _AddNewLincnseClasses()
{
this.LicenseClassID = clsLincnseClassesDataAccess.AddLincnseClasses(LincnseClassDTO);
return (this.LicenseClassID != -1);

}

private bool _UpdateLincnseClasses()
{
return clsLincnseClassesDataAccess.UpdateLincnseClasses(LincnseClassDTO);
}

public static clsLincnseClasses Find(int Id)
{
LincnseClassesDTO LDTO = clsLincnseClassesDataAccess.GetLincnseClassesInfoByID(Id);

if(LDTO != null)
return new clsLincnseClasses(LDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLincnseClasses())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLincnseClasses();

            }




            return false;
        }

public static List<LincnseClassesDTO> GetAllLincnseClasses(){return clsLincnseClassesDataAccess.GetAllLincnseClasses();}

public static bool DeleteLincnseClasses(int LicenseClassID){return  clsLincnseClassesDataAccess.DeleteLincnseClasses(LicenseClassID);}

public static bool isLicenseClassExist(int LicenseClassID){return clsLincnseClassesDataAccess.IsLincnseClassesExist(LicenseClassID);}


}

}