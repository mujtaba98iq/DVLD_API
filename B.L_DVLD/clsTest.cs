using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace TestsBusinessLayer
{

public class clsTests
{
 public TestsDTO TestDTO
  {get { return (new TestsDTO(this.TestID,this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int TestID {get; set;}
public int TestAppointmentID {get; set;}
public bool TestResult {get; set;}
public string Notes {get; set;}
public int CreatedByUserID {get; set;}


public clsTests()
{
this.TestID = default;
this.TestAppointmentID = default;
this.TestResult = default;
this.Notes = default;
this.CreatedByUserID = default;


Mode = enMode.AddNew;

}

public clsTests(TestsDTO TDTO, enMode cMode =enMode.AddNew) 
{
this.TestID = TDTO.TestID;
this.TestAppointmentID = TDTO.TestAppointmentID;
this.TestResult = TDTO.TestResult;
this.Notes = TDTO.Notes;
this.CreatedByUserID = TDTO.CreatedByUserID;


Mode = cMode;

}

private bool _AddNewTests()
{
this.TestID = clsTestsDataAccess.AddTests(TestDTO);
return (this.TestID != -1);

}

private bool _UpdateTests()
{
return clsTestsDataAccess.UpdateTests(TestDTO);
}

public static clsTests Find(int Id)
{
TestsDTO TDTO = clsTestsDataAccess.GetTestsInfoByID(Id);

if(TDTO != null)
return new clsTests(TDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTests())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTests();

            }




            return false;
        }

public static List<TestsDTO> GetAllTests(){return clsTestsDataAccess.GetAllTests();}

public static bool DeleteTests(int TestID){return  clsTestsDataAccess.DeleteTests(TestID);}

public static async Task <bool> isTestExist(int TestID){return await clsTestsDataAccess.IsTestsExistAsync(TestID);}


}

}