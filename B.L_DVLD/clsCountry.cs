using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace CountriesBusinessLayer
{

public class clsCountries
{
 public CountriesDTO CountryDTO
        { get { return (new CountriesDTO(this.CountryID,this.CountryName)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int CountryID {get; set;}
public string CountryName {get; set;}


public clsCountries()
{
this.CountryID = default;
this.CountryName = default;


Mode = enMode.AddNew;

}

public clsCountries(CountriesDTO CDTO, enMode cMode =enMode.AddNew) 
{
this.CountryID = CDTO.CountryID;
this.CountryName = CDTO.CountryName;


Mode = cMode;

}

private bool _AddNewCountries()
{
this.CountryID = clsCountriesDataAccess.AddCountries(CountryDTO);
return (this.CountryID != -1);

}

private bool _UpdateCountries()
{
return clsCountriesDataAccess.UpdateCountries(CountryDTO);
}

public static clsCountries Find(int Id)
{
CountriesDTO CDTO = clsCountriesDataAccess.GetCountriesInfoByID(Id);

if(CDTO != null)
return new clsCountries(CDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountries())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCountries();

            }




            return false;
        }

public static List<CountriesDTO> GetAllCountries(){return clsCountriesDataAccess.GetAllCountries();}

public static bool DeleteCountries(int CountryID){return  clsCountriesDataAccess.DeleteCountries(CountryID);}

public static bool isCountryExist(int CountryID){return clsCountriesDataAccess.IsCountriesExist(CountryID);}


}

}