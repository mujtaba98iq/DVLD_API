using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace DriversBusinessLayer
{

public class clsDrivers
{
 public DriversDTO DriverDTO
        { get { return (new DriversDTO(this.DriverID,this.PersonID,this.CreatedByUserID,this.CreatedDate)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int DriverID {get; set;}
public int PersonID {get; set;}
public int CreatedByUserID {get; set;}
public DateTime CreatedDate {get; set;}


public clsDrivers()
{
this.DriverID = default;
this.PersonID = default;
this.CreatedByUserID = default;
this.CreatedDate = default;


Mode = enMode.AddNew;

}

public clsDrivers(DriversDTO DDTO, enMode cMode =enMode.AddNew) 
{
this.DriverID = DDTO.DriverID;
this.PersonID = DDTO.PersonID;
this.CreatedByUserID = DDTO.CreatedByUserID;
this.CreatedDate = DDTO.CreatedDate;


Mode = cMode;

}

private bool _AddNewDrivers()
{
this.DriverID = clsDriversDataAccess.AddDrivers(DriverDTO);
return (this.DriverID != -1);

}

private bool _UpdateDrivers()
{
return clsDriversDataAccess.UpdateDrivers(DriverDTO);
}

public static clsDrivers Find(int Id)
{
DriversDTO DDTO = clsDriversDataAccess.GetDriversInfoByID(Id);

if(DDTO != null)
return new clsDrivers(DDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDrivers())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDrivers();

            }




            return false;
        }

public static List<DriversDTO> GetAllDrivers(){return clsDriversDataAccess.GetAllDrivers();}

public static bool DeleteDrivers(int DriverID){return  clsDriversDataAccess.DeleteDrivers(DriverID);}

public static bool isDriverExist(int DriverID){return clsDriversDataAccess.GetDriversInfoByID(DriverID)!=null;}


}

}