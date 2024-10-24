using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class LocalDrivingLicenseApplicationsDTO
{
public int LocalLicenseApplicationID {get; set;}
public int ApplicationID {get; set;}
public int LicenseClassID {get; set;}


public LocalDrivingLicenseApplicationsDTO ( int LocalLicenseApplicationID,  int ApplicationID,  int LicenseClassID)
{
this.LocalLicenseApplicationID = LocalLicenseApplicationID;
this.ApplicationID = ApplicationID;
this.LicenseClassID = LicenseClassID;


}


}


public static class clsLocalDrivingLicenseApplicationsDataAccess
{
public static LocalDrivingLicenseApplicationsDTO GetLocalDrivingLicenseApplicationsInfoByID(int LocalLicenseApplicationID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetLocalDrivingLicenseApplicationsInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new LocalDrivingLicenseApplicationsDTO

(
			reader.GetInt32(reader.GetOrdinal("LocalLicenseApplicationID")),			reader.GetInt32(reader.GetOrdinal("ApplicationID")),			reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
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
public static int AddLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationsDTO LocalDrivingLicenseApplicationsDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"[SP_AddLocalDrivingLicenseApplications]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseApplicationsDTO.ApplicationID );

	command.Parameters.AddWithValue("@LicenseClassID", LocalDrivingLicenseApplicationsDTO.LicenseClassID );



                var outputIdParam = new SqlParameter("@NewLocalDrivingLicenseApplicationsId", SqlDbType.Int)
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


public static bool UpdateLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationsDTO LocalDrivingLicenseApplicationsDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))


{
	string query = "[SP_UpdateLocalDrivingLicenseApplications]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID );

	command.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseApplicationsDTO.ApplicationID );

	command.Parameters.AddWithValue("@LicenseClassID", LocalDrivingLicenseApplicationsDTO.LicenseClassID );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
        public static bool DeleteLocalDrivingLicenseApplications(int localLicenseApplicationID)
        {
            if (localLicenseApplicationID <= 0)
            {
                throw new ArgumentException("The LocalLicenseApplicationID must be greater than zero.", nameof(localLicenseApplicationID));
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("[SP_DeleteLocalDrivingLicenseApplications]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LocalLicenseApplicationID", localLicenseApplicationID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected == 1;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                clsErrorHandling.HandleError(sqlEx.ToString());
                return false; // or rethrow if you want to handle it further up
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                return false; // or rethrow if you want to handle it further up
            }
        }


        public static bool IsLocalDrivingLicenseApplicationsExist(int LocalLicenseApplicationID)
{
	bool isFound = false;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_IsLocalDrivingLicenseApplicationsExist]"; 
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@LocalLicenseApplicationID", LocalLicenseApplicationID );

		connection.Open();
                        var result = command.ExecuteScalar();
                        isFound = (result != null && (int)result > 0);
                    
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return isFound;

}

public static List<LocalDrivingLicenseApplicationsDTO> GetAllLocalDrivingLicenseApplications()
{

 var LocalDrivingLicenseApplicationsList = new List<LocalDrivingLicenseApplicationsDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_GetAllLocalDrivingLicenseApplications]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		LocalDrivingLicenseApplicationsList.Add(new LocalDrivingLicenseApplicationsDTO
				(
			reader.GetInt32(reader.GetOrdinal("LocalLicenseApplicationID")),			reader.GetInt32(reader.GetOrdinal("ApplicationID")),			reader.GetInt32(reader.GetOrdinal("LicenseClassID"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return LocalDrivingLicenseApplicationsList;
}


}

}