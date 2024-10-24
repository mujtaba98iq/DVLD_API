using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
   
    public class LocalDrivingLicenseFullApplications_ViewDTO
{
public int ApplicationID {get; set;}
public int ApplicantPersonID {get; set;}
public DateTime ApplicationDate {get; set;}
public int ApplicationTypeID {get; set;}
public byte ApplicationStatus {get; set;}
public DateTime LastStatusDate {get; set;}
public decimal PaidFees {get; set;}
public int CreatedByUserID {get; set;}
public int LocalDrivingLicenseApplicationID {get; set;}
public int LicenseClassID {get; set;}


public LocalDrivingLicenseFullApplications_ViewDTO ( int ApplicationID,  int ApplicantPersonID,  DateTime ApplicationDate,  int ApplicationTypeID,  byte ApplicationStatus,  DateTime LastStatusDate,  decimal PaidFees,  int CreatedByUserID,  int LocalDrivingLicenseApplicationID,  int LicenseClassID)
{
this.ApplicationID = ApplicationID;
this.ApplicantPersonID = ApplicantPersonID;
this.ApplicationDate = ApplicationDate;
this.ApplicationTypeID = ApplicationTypeID;
this.ApplicationStatus = ApplicationStatus;
this.LastStatusDate = LastStatusDate;
this.PaidFees = PaidFees;
this.CreatedByUserID = CreatedByUserID;
this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
this.LicenseClassID = LicenseClassID;
}

}


public static class clsLocalDrivingLicenseFullApplications_ViewDataAccess
{
public static LocalDrivingLicenseFullApplications_ViewDTO GetLocalDrivingLicenseFullApplications_ViewInfoByID(int ApplicationID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "SELECT * FROM LocalDrivingLicenseFullApplications_View WHERE ApplicationID = @ApplicationID";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new LocalDrivingLicenseFullApplications_ViewDTO

(
			reader.GetInt32(reader.GetOrdinal("ApplicationID")),			reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),			reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),			reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),			reader.GetByte(reader.GetOrdinal("ApplicationStatus")),			reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),			reader.GetDecimal(reader.GetOrdinal("PaidFees")),			reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),			reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),			reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
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
public static int AddLocalDrivingLicenseFullApplications_View(LocalDrivingLicenseFullApplications_ViewDTO LocalDrivingLicenseFullApplications_ViewDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"INSERT INTO LocalDrivingLicenseFullApplications_View VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID, @LocalDrivingLicenseApplicationID, @LicenseClassID)
        SELECT SCOPE_IDENTITY()";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@ApplicantPersonID", LocalDrivingLicenseFullApplications_ViewDTO.ApplicantPersonID );

	command.Parameters.AddWithValue("@ApplicationDate", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationDate );

	command.Parameters.AddWithValue("@ApplicationTypeID", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationTypeID );

	command.Parameters.AddWithValue("@ApplicationStatus", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationStatus );

	command.Parameters.AddWithValue("@LastStatusDate", LocalDrivingLicenseFullApplications_ViewDTO.LastStatusDate );

	command.Parameters.AddWithValue("@PaidFees", LocalDrivingLicenseFullApplications_ViewDTO.PaidFees );

	command.Parameters.AddWithValue("@CreatedByUserID", LocalDrivingLicenseFullApplications_ViewDTO.CreatedByUserID );

	command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseFullApplications_ViewDTO.LocalDrivingLicenseApplicationID );

	command.Parameters.AddWithValue("@LicenseClassID", LocalDrivingLicenseFullApplications_ViewDTO.LicenseClassID );



                var outputIdParam = new SqlParameter("@NewLocalDrivingLicenseFullApplications_ViewId", SqlDbType.Int)
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


public static bool UpdateLocalDrivingLicenseFullApplications_View(LocalDrivingLicenseFullApplications_ViewDTO LocalDrivingLicenseFullApplications_ViewDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"UPDATE LocalDrivingLicenseFullApplications_View
	SET	ApplicantPersonID = @ApplicantPersonID,
	ApplicationDate = @ApplicationDate,
	ApplicationTypeID = @ApplicationTypeID,
	ApplicationStatus = @ApplicationStatus,
	LastStatusDate = @LastStatusDate,
	PaidFees = @PaidFees,
	CreatedByUserID = @CreatedByUserID,
	LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
	LicenseClassID = @LicenseClassID	WHERE ApplicationID = @ApplicationID";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationID );

	command.Parameters.AddWithValue("@ApplicantPersonID", LocalDrivingLicenseFullApplications_ViewDTO.ApplicantPersonID );

	command.Parameters.AddWithValue("@ApplicationDate", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationDate );

	command.Parameters.AddWithValue("@ApplicationTypeID", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationTypeID );

	command.Parameters.AddWithValue("@ApplicationStatus", LocalDrivingLicenseFullApplications_ViewDTO.ApplicationStatus );

	command.Parameters.AddWithValue("@LastStatusDate", LocalDrivingLicenseFullApplications_ViewDTO.LastStatusDate );

	command.Parameters.AddWithValue("@PaidFees", LocalDrivingLicenseFullApplications_ViewDTO.PaidFees );

	command.Parameters.AddWithValue("@CreatedByUserID", LocalDrivingLicenseFullApplications_ViewDTO.CreatedByUserID );

	command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseFullApplications_ViewDTO.LocalDrivingLicenseApplicationID );

	command.Parameters.AddWithValue("@LicenseClassID", LocalDrivingLicenseFullApplications_ViewDTO.LicenseClassID );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteLocalDrivingLicenseFullApplications_View(int ApplicationID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "DELETE LocalDrivingLicenseFullApplications_View WHERE ApplicationID = @ApplicationID";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@ApplicationID", ApplicationID );

		connection.Open();
		rowsAffected = (int)command.ExecuteScalar();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

public static bool IsLocalDrivingLicenseFullApplications_ViewExist(int ApplicationID)
{
	bool isFound = false;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "SELECT Found=1 FROM LocalDrivingLicenseFullApplications_View WHERE ApplicationID= @ApplicationID"; 
using(	SqlCommand command = new SqlCommand(query, connection)) 
{

	command.Parameters.AddWithValue("@ApplicationID", ApplicationID );

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

public static List<LocalDrivingLicenseFullApplications_ViewDTO> GetAllLocalDrivingLicenseFullApplications_View()
{

 var LocalDrivingLicenseFullApplications_ViewList = new List<LocalDrivingLicenseFullApplications_ViewDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "SELECT * FROM LocalDrivingLicenseFullApplications_View";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		LocalDrivingLicenseFullApplications_ViewList.Add(new LocalDrivingLicenseFullApplications_ViewDTO
				(
			reader.GetInt32(reader.GetOrdinal("ApplicationID")),			reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),			reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),			reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),			reader.GetByte(reader.GetOrdinal("ApplicationStatus")),			reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),			reader.GetDecimal(reader.GetOrdinal("PaidFees")),			reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),			reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),			reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return LocalDrivingLicenseFullApplications_ViewList;
}


}

}