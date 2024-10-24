using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace ApplcatoinTypesBusinessLayer
{

public class clsApplcatoinTypes
{
 public ApplcatoinTypesDTO ApplcatoinTypeDTO
        { get { return (new ApplcatoinTypesDTO(this.ApplicationTypeID,this.ApplicationTypeTitle,this.ApplicationFees)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int ApplicationTypeID {get; set;}
public string ApplicationTypeTitle {get; set;}
public decimal ApplicationFees {get; set;}


public clsApplcatoinTypes()
{
this.ApplicationTypeID = default;
this.ApplicationTypeTitle = default;
this.ApplicationFees = default;


Mode = enMode.AddNew;

}

public clsApplcatoinTypes(ApplcatoinTypesDTO ADTO, enMode cMode =enMode.AddNew) 
{
this.ApplicationTypeID = ADTO.ApplicationTypeID;
this.ApplicationTypeTitle = ADTO.ApplicationTypeTitle;
this.ApplicationFees = ADTO.ApplicationFees;


Mode = cMode;

}

private bool _AddNewApplcatoinTypes()
{
this.ApplicationTypeID = clsApplcatoinTypesDataAccess.AddApplcatoinTypes(ApplcatoinTypeDTO);
return (this.ApplicationTypeID != -1);

}

private bool _UpdateApplcatoinTypes()
{
return clsApplcatoinTypesDataAccess.UpdateApplcatoinTypes(ApplcatoinTypeDTO);
}

public static clsApplcatoinTypes Find(int Id)
{
ApplcatoinTypesDTO ADTO = clsApplcatoinTypesDataAccess.GetApplcatoinTypesInfoByID(Id);

if(ADTO != null)
return new clsApplcatoinTypes(ADTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplcatoinTypes())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplcatoinTypes();

            }




            return false;
        }

public static List<ApplcatoinTypesDTO> GetAllApplcatoinTypes(){return clsApplcatoinTypesDataAccess.GetAllApplcatoinTypes();}

public static bool DeleteApplcatoinTypes(int ApplicationTypeID){return  clsApplcatoinTypesDataAccess.DeleteApplcatoinTypes(ApplicationTypeID);}

public static async Task <bool> isApplicationTypeExist(int ApplicationTypeID){return await clsApplcatoinTypesDataAccess.IsApplicationTypesExistAsync(ApplicationTypeID);}


}

}