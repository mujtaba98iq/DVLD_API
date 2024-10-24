using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace PeopleBusinessLayer
{

public class clsPeople
{
 public PeopleDTO PersonDTO
  {get { return (new PeopleDTO(this.PersonID,this.NationalNo,this.FirstName,this.SecondName,this.ThirdName,this.LastName,this.DateOfBirth,this.Gendor,this.Address,this.Phone,this.Email,this.NationalityCountryID,this.ImagePath)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int PersonID {get; set;}
public string NationalNo {get; set;}
public string FirstName {get; set;}
public string SecondName {get; set;}
public string ThirdName {get; set;}
public string LastName {get; set;}
public DateTime DateOfBirth {get; set;}
public byte Gendor {get; set;}
public string Address {get; set;}
public string Phone {get; set;}
public string Email {get; set;}
public int NationalityCountryID {get; set;}
public string ImagePath {get; set;}


public clsPeople()
{
this.PersonID = default;
this.NationalNo = default;
this.FirstName = default;
this.SecondName = default;
this.ThirdName = default;
this.LastName = default;
this.DateOfBirth = default;
this.Gendor = default;
this.Address = default;
this.Phone = default;
this.Email = default;
this.NationalityCountryID = default;
this.ImagePath = default;


Mode = enMode.AddNew;

}

public clsPeople(PeopleDTO PDTO, enMode cMode =enMode.AddNew) 
{
this.PersonID = PDTO.PersonID;
this.NationalNo = PDTO.NationalNo;
this.FirstName = PDTO.FirstName;
this.SecondName = PDTO.SecondName;
this.ThirdName = PDTO.ThirdName;
this.LastName = PDTO.LastName;
this.DateOfBirth = PDTO.DateOfBirth;
this.Gendor = PDTO.Gendor;
this.Address = PDTO.Address;
this.Phone = PDTO.Phone;
this.Email = PDTO.Email;
this.NationalityCountryID = PDTO.NationalityCountryID;
this.ImagePath = PDTO.ImagePath;


Mode = cMode;

}

private bool _AddNewPeople()
{
this.PersonID = clsPeopleDataAccess.AddPeople(PersonDTO);
return (this.PersonID != -1);

}

private bool _UpdatePeople()
{
return clsPeopleDataAccess.UpdatePeople(PersonDTO);
}

public static clsPeople Find(int Id)
{
PeopleDTO PDTO = clsPeopleDataAccess.GetPeopleInfoByID(Id);

if(PDTO != null)
return new clsPeople(PDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPeople())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePeople();

            }




            return false;
        }

public static List<PeopleDTO> GetAllPeople(){return clsPeopleDataAccess.GetAllPeople();}

public static bool DeletePeople(int PersonID){return  clsPeopleDataAccess.DeletePeople(PersonID);}

        public static async Task<bool> IsPersonExistAsync(int personID)
        {
            return await clsPeopleDataAccess.IsPeopleExistAsync(personID);
        }



    }

}