using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class ApplicationsDTO
{
public int ApplicationID {get; set;}
public int ApplicantPersonID {get; set;}
public DateTime ApplicationDate {get; set;}
public int ApplicationTypeID {get; set;}
public byte ApplicationStatus {get; set;}
public DateTime LastStatusDate {get; set;}
public decimal PaidFees {get; set;}
public int CreatedByUserID {get; set;}


public ApplicationsDTO ( int ApplicationID,  int ApplicantPersonID,  DateTime ApplicationDate,  int ApplicationTypeID,  byte ApplicationStatus,  DateTime LastStatusDate,  decimal PaidFees,  int CreatedByUserID)
{
this.ApplicationID = ApplicationID;
this.ApplicantPersonID = ApplicantPersonID;
this.ApplicationDate = ApplicationDate;
this.ApplicationTypeID = ApplicationTypeID;
this.ApplicationStatus = ApplicationStatus;
this.LastStatusDate = LastStatusDate;
this.PaidFees = PaidFees;
this.CreatedByUserID = CreatedByUserID;


}


}


public static class clsApplicationsDataAccess
{
public static ApplicationsDTO GetApplicationsInfoByID(int ApplicationID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetApplicationsInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new ApplicationsDTO

(
			reader.GetInt32(reader.GetOrdinal("ApplicationID")),		
			reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),	
			reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),	
			reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),	
			reader.GetByte(reader.GetOrdinal("ApplicationStatus")),		
			reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),	
			reader.GetDecimal(reader.GetOrdinal("PaidFees")),		
			reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
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
public static int AddApplications(ApplicationsDTO ApplicationsDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = "[SP_AddApplications]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationsDTO.ApplicantPersonID );
						
	command.Parameters.AddWithValue("@ApplicationDate", ApplicationsDTO.ApplicationDate );

	command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationsDTO.ApplicationTypeID );

	command.Parameters.AddWithValue("@ApplicationStatus", ApplicationsDTO.ApplicationStatus );

	command.Parameters.AddWithValue("@LastStatusDate", ApplicationsDTO.LastStatusDate );

	command.Parameters.AddWithValue("@PaidFees", ApplicationsDTO.PaidFees );

	command.Parameters.AddWithValue("@CreatedByUserID", ApplicationsDTO.CreatedByUserID );



                var outputIdParam = new SqlParameter("@NewApplicationsId", SqlDbType.Int)
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

        public static bool UpdateApplications(ApplicationsDTO ApplicationsDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_UpdateApplications]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // إضافة المعاملات مع التأكد من التحقق من القيم
                        command.Parameters.Add(new SqlParameter("@ApplicationID", SqlDbType.Int)).Value = ApplicationsDTO.ApplicationID;
                        command.Parameters.Add(new SqlParameter("@ApplicantPersonID", SqlDbType.Int)).Value = ApplicationsDTO.ApplicantPersonID;
                        command.Parameters.Add(new SqlParameter("@ApplicationDate", SqlDbType.DateTime)).Value = ApplicationsDTO.ApplicationDate;
                        command.Parameters.Add(new SqlParameter("@ApplicationTypeID", SqlDbType.Int)).Value = ApplicationsDTO.ApplicationTypeID;
                        command.Parameters.Add(new SqlParameter("@ApplicationStatus", SqlDbType.NVarChar, 50)).Value = ApplicationsDTO.ApplicationStatus;

                        // التحقق من التاريخ إذا كان يمكن أن يكون null
                        command.Parameters.Add(new SqlParameter("@LastStatusDate", SqlDbType.DateTime)).Value = (object)ApplicationsDTO.LastStatusDate ?? DBNull.Value;

                        command.Parameters.Add(new SqlParameter("@PaidFees", SqlDbType.Decimal)).Value = ApplicationsDTO.PaidFees;
                        command.Parameters.Add(new SqlParameter("@CreatedByUserID", SqlDbType.Int)).Value = ApplicationsDTO.CreatedByUserID;

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteApplications(int ApplicationID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_DeleteApplications]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        // Use ExecuteNonQuery for DELETE operations
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            // Return true if at least one row was deleted
            return (rowsAffected > 0);
        }


        public static bool IsApplicationsExist(int ApplicationID)
{
	bool isFound = false;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_IsApplicationsExist]"; 
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
                        command.CommandType = CommandType.StoredProcedure;

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

public static List<ApplicationsDTO> GetAllApplications()
{

 var ApplicationsList = new List<ApplicationsDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_GetAllApplications]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		ApplicationsList.Add(new ApplicationsDTO
				(
			reader.GetInt32(reader.GetOrdinal("ApplicationID")),			reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),			reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),			reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),			reader.GetByte(reader.GetOrdinal("ApplicationStatus")),			reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),			reader.GetDecimal(reader.GetOrdinal("PaidFees")),			reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return ApplicationsList;
}


}

}