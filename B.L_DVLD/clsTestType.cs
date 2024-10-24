using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace TestTypesBusinessLayer
{

public class clsTestTypes
{
 public TestTypesDTO TestTypeDTO
        { get { return (new TestTypesDTO(this.TestTypeID,this.TestTypeTitle,this.TestTypeDescription,this.TestFees)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int TestTypeID {get; set;}
public string TestTypeTitle {get; set;}
public string TestTypeDescription {get; set;}
public decimal TestFees {get; set;}


public clsTestTypes()
{
this.TestTypeID = default;
this.TestTypeTitle = default;
this.TestTypeDescription = default;
this.TestFees = default;


Mode = enMode.AddNew;

}

public clsTestTypes(TestTypesDTO TDTO, enMode cMode =enMode.AddNew) 
{
this.TestTypeID = TDTO.TestTypeID;
this.TestTypeTitle = TDTO.TestTypeTitle;
this.TestTypeDescription = TDTO.TestTypeDescription;
this.TestFees = TDTO.TestFees;


Mode = cMode;

}

private bool _AddNewTestTypes()
{
this.TestTypeID = clsTestTypesDataAccess.AddTestTypes(TestTypeDTO);
return (this.TestTypeID != -1);

}

private bool _UpdateTestTypes()
{
return clsTestTypesDataAccess.UpdateTestTypes(TestTypeDTO);
}

public static clsTestTypes Find(int Id)
{
TestTypesDTO TDTO = clsTestTypesDataAccess.GetTestTypesInfoByID(Id);

if(TDTO != null)
return new clsTestTypes(TDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestTypes())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestTypes();

            }




            return false;
        }

public static List<TestTypesDTO> GetAllTestTypes(){return clsTestTypesDataAccess.GetAllTestTypes();}

public static bool DeleteTestTypes(int TestTypeID){return  clsTestTypesDataAccess.DeleteTestTypes(TestTypeID);}

public static async Task< bool> isTestTypeExist(int TestTypeID){return await clsTestTypesDataAccess.IsTestTypesExistAsync(TestTypeID);}


}

}