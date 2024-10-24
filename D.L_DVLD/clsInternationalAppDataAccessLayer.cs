using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class InternationalLicensesDTO
{
public int InternationalAppID {get; set;}
public int ApplicatoinID {get; set;}
public int DirverID {get; set;}
public int LocalDriverApplicationID {get; set;}
public DateTime IssueDate {get; set;}
public DateTime ExpirationDate {get; set;}
public bool IsActive {get; set;}
public int CreatedByUserID {get; set;}


public InternationalLicensesDTO ( int InternationalAppID,  int ApplicatoinID,  int DirverID,  int LocalDriverApplicationID,  DateTime IssueDate,  DateTime ExpirationDate,  bool IsActive,  int CreatedByUserID)
{
this.InternationalAppID = InternationalAppID;
this.ApplicatoinID = ApplicatoinID;
this.DirverID = DirverID;
this.LocalDriverApplicationID = LocalDriverApplicationID;
this.IssueDate = IssueDate;
this.ExpirationDate = ExpirationDate;
this.IsActive = IsActive;
this.CreatedByUserID = CreatedByUserID;


}


}


public static class clsInternationalLicensesDataAccess
{
public static InternationalLicensesDTO GetInternationalLicensesInfoByID(int InternationalAppID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetInternationalLicensesInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new InternationalLicensesDTO

(
			reader.GetInt32(reader.GetOrdinal("InternationalAppID")),			reader.GetInt32(reader.GetOrdinal("ApplicatoinID")),			reader.GetInt32(reader.GetOrdinal("DirverID")),			reader.GetInt32(reader.GetOrdinal("LocalDriverApplicationID")),			reader.GetDateTime(reader.GetOrdinal("IssueDate")),			reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),			reader.GetBoolean(reader.GetOrdinal("IsActive")),			reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
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
public static int AddInternationalLicenses(InternationalLicensesDTO InternationalLicensesDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = "[SP_AddInternationalLicenses]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@ApplicatoinID", InternationalLicensesDTO.ApplicatoinID );

	command.Parameters.AddWithValue("@DirverID", InternationalLicensesDTO.DirverID );

	command.Parameters.AddWithValue("@LocalDriverApplicationID", InternationalLicensesDTO.LocalDriverApplicationID );

	command.Parameters.AddWithValue("@IssueDate", InternationalLicensesDTO.IssueDate );

	command.Parameters.AddWithValue("@ExpirationDate", InternationalLicensesDTO.ExpirationDate );

	command.Parameters.AddWithValue("@IsActive", InternationalLicensesDTO.IsActive );

	command.Parameters.AddWithValue("@CreatedByUserID", InternationalLicensesDTO.CreatedByUserID );



                var outputIdParam = new SqlParameter("@NewInternationalLicensesId", SqlDbType.Int)
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


public static bool UpdateInternationalLicenses(InternationalLicensesDTO InternationalLicensesDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"[SP_UpdateInternationalLicenses]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@InternationalAppID", InternationalLicensesDTO.InternationalAppID );

	command.Parameters.AddWithValue("@ApplicatoinID", InternationalLicensesDTO.ApplicatoinID );

	command.Parameters.AddWithValue("@DirverID", InternationalLicensesDTO.DirverID );

	command.Parameters.AddWithValue("@LocalDriverApplicationID", InternationalLicensesDTO.LocalDriverApplicationID );

	command.Parameters.AddWithValue("@IssueDate", InternationalLicensesDTO.IssueDate );

	command.Parameters.AddWithValue("@ExpirationDate", InternationalLicensesDTO.ExpirationDate );

	command.Parameters.AddWithValue("@IsActive", InternationalLicensesDTO.IsActive );

	command.Parameters.AddWithValue("@CreatedByUserID", InternationalLicensesDTO.CreatedByUserID );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteInternationalLicenses(int InternationalAppID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteInternationalLicenses]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID );

		connection.Open();
		rowsAffected = (int)command.ExecuteScalar();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

public static bool IsInternationalLicensesExist(int InternationalAppID)
{
	bool isFound = false;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "SELECT Found=1 FROM InternationalLicenses WHERE InternationalAppID= @InternationalAppID"; 
using(	SqlCommand command = new SqlCommand(query, connection)) 
{

	command.Parameters.AddWithValue("@InternationalAppID", InternationalAppID );

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

public static List<InternationalLicensesDTO> GetAllInternationalLicenses()
{

 var InternationalLicensesList = new List<InternationalLicensesDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_GetAllInternationalLicenses]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		InternationalLicensesList.Add(new InternationalLicensesDTO
				(
			reader.GetInt32(reader.GetOrdinal("InternationalAppID")),			reader.GetInt32(reader.GetOrdinal("ApplicatoinID")),			reader.GetInt32(reader.GetOrdinal("DirverID")),			reader.GetInt32(reader.GetOrdinal("LocalDriverApplicationID")),			reader.GetDateTime(reader.GetOrdinal("IssueDate")),			reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),			reader.GetBoolean(reader.GetOrdinal("IsActive")),			reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return InternationalLicensesList;
}


}

}