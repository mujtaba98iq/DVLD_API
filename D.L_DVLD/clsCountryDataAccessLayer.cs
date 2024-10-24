using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class CountriesDTO
{
public int CountryID {get; set;}
public string CountryName {get; set;}


public CountriesDTO ( int CountryID,  string CountryName)
{
this.CountryID = CountryID;
this.CountryName = CountryName;


}


}


public static class clsCountriesDataAccess
{
public static CountriesDTO GetCountriesInfoByID(int CountryID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetCountriesInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@CountryID", CountryID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new CountriesDTO

(
			reader.GetInt32(reader.GetOrdinal("CountryID")),	
			reader.GetString(reader.GetOrdinal("CountryName"))
);
		}
		else
		{
			return null;
		}
}
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

 return null;

}
public static int AddCountries(CountriesDTO CountriesDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"INSERT INTO Countries VALUES (@CountryName)
        SELECT SCOPE_IDENTITY()";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@CountryName", CountriesDTO.CountryName );



                var outputIdParam = new SqlParameter("@NewCountriesId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
               command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();
            ID=(int)outputIdParam.Value;

        }          
  	}
}
			catch (Exception ex) { clsErrorHandling.HandleError(ex.ToString()); }
            return ID;

}


public static bool UpdateCountries(CountriesDTO CountriesDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"UPDATE Countries
	SET	CountryName = @CountryName	WHERE CountryID = @CountryID";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@CountryID", CountriesDTO.CountryID );

	command.Parameters.AddWithValue("@CountryName", CountriesDTO.CountryName );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteCountries(int CountryID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "DELETE Countries WHERE CountryID = @CountryID";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@CountryID", CountryID );

		connection.Open();
		rowsAffected = (int)command.ExecuteScalar();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

public static bool IsCountriesExist(int CountryID)
{
	bool isFound = false;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_IsCountriesExist]"; 
using(	SqlCommand command = new SqlCommand(query, connection)) 
{

	command.Parameters.AddWithValue("@CountryID", CountryID );

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
			isFound = reader.HasRows;
}
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;

}

public static List<CountriesDTO> GetAllCountries()
{

 var CountriesList = new List<CountriesDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_GetAllCountries]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		CountriesList.Add(new CountriesDTO
				(
			reader.GetInt32(reader.GetOrdinal("CountryID")),			reader.GetString(reader.GetOrdinal("CountryName"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return CountriesList;
}


}

}