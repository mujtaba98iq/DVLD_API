using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class LincnseClassesDTO
{
public int LicenseClassID {get; set;}
public string ClassName {get; set;}
public string ClassDescription {get; set;}
public byte MinimumAllowedAge {get; set;}
public byte DefaultValidityLength {get; set;}
public decimal ClassFees {get; set;}


public LincnseClassesDTO ( int LicenseClassID,  string ClassName,  string ClassDescription,  byte MinimumAllowedAge,  byte DefaultValidityLength,  decimal ClassFees)
{
this.LicenseClassID = LicenseClassID;
this.ClassName = ClassName;
this.ClassDescription = ClassDescription;
this.MinimumAllowedAge = MinimumAllowedAge;
this.DefaultValidityLength = DefaultValidityLength;
this.ClassFees = ClassFees;


}


}


public static class clsLincnseClassesDataAccess
{
public static LincnseClassesDTO GetLincnseClassesInfoByID(int LicenseClassID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetLincnseClassesInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new LincnseClassesDTO

(
			reader.GetInt32(reader.GetOrdinal("LicenseClassID")),			reader.GetString(reader.GetOrdinal("ClassName")),			reader.GetString(reader.GetOrdinal("ClassDescription")),			reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),			reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),			reader.GetDecimal(reader.GetOrdinal("ClassFees"))
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
public static int AddLincnseClasses(LincnseClassesDTO LincnseClassesDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"[SP_AddLincnseClasses]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@ClassName", LincnseClassesDTO.ClassName );

	command.Parameters.AddWithValue("@ClassDescription", LincnseClassesDTO.ClassDescription );

	command.Parameters.AddWithValue("@MinimumAllowedAge", LincnseClassesDTO.MinimumAllowedAge );

	command.Parameters.AddWithValue("@DefaultValidityLength", LincnseClassesDTO.DefaultValidityLength );

	command.Parameters.AddWithValue("@ClassFees", LincnseClassesDTO.ClassFees );



                var outputIdParam = new SqlParameter("@NewLincnseClassesId", SqlDbType.Int)
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


public static bool UpdateLincnseClasses(LincnseClassesDTO LincnseClassesDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = "[SP_UpdateLincnseClasses]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@LicenseClassID", LincnseClassesDTO.LicenseClassID );

	command.Parameters.AddWithValue("@ClassName", LincnseClassesDTO.ClassName );

	command.Parameters.AddWithValue("@ClassDescription", LincnseClassesDTO.ClassDescription );

	command.Parameters.AddWithValue("@MinimumAllowedAge", LincnseClassesDTO.MinimumAllowedAge );

	command.Parameters.AddWithValue("@DefaultValidityLength", LincnseClassesDTO.DefaultValidityLength );

	command.Parameters.AddWithValue("@ClassFees", LincnseClassesDTO.ClassFees );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteLincnseClasses(int LicenseClassID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteLincnseClasses]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

public static bool IsLincnseClassesExist(int LicenseClassID)
{
	bool isFound = false;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_IsLincnseClassesExist]"; 
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID );

		connection.Open();
						var result = command.ExecuteScalar();
                        isFound = (result != null && (int)result > 0);
                    }
}

}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;

}

public static List<LincnseClassesDTO> GetAllLincnseClasses()
{

 var LincnseClassesList = new List<LincnseClassesDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_GetAllLincnseClasses]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		LincnseClassesList.Add(new LincnseClassesDTO
				(
			reader.GetInt32(reader.GetOrdinal("LicenseClassID")),			reader.GetString(reader.GetOrdinal("ClassName")),			reader.GetString(reader.GetOrdinal("ClassDescription")),			reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),			reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),			reader.GetDecimal(reader.GetOrdinal("ClassFees"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return LincnseClassesList;
}


}

}