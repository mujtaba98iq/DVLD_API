using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
    public class DetainedLicensesDTO
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; } // تعديل هنا لجعل الحقل nullable
        public int? ReleasedByUserID { get; set; }
        public int? ReleaseApplicationID { get; set; }

        public DetainedLicensesDTO(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID, bool isReleased, DateTime? releaseDate, int? releasedByUserID, int? releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }
    }



    public static class clsDetainedLicensesDataAccess
{
public static DetainedLicensesDTO GetDetainedLicensesInfoByID(int DetainID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetDetainedLicensesInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@DetainID", DetainID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new DetainedLicensesDTO

(
			reader.GetInt32(reader.GetOrdinal("DetainID")),			reader.GetInt32(reader.GetOrdinal("LicenseID")),			reader.GetDateTime(reader.GetOrdinal("DetainDate")),			reader.GetDecimal(reader.GetOrdinal("FineFees")),			reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),			reader.GetBoolean(reader.GetOrdinal("IsReleased")),			reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),			reader.GetInt32(reader.GetOrdinal("ReleasedByUserID")),			reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID"))
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
public static int AddDetainedLicenses(DetainedLicensesDTO DetainedLicensesDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = "[SP_AddDetainedLicenses]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@LicenseID", DetainedLicensesDTO.LicenseID );

	command.Parameters.AddWithValue("@DetainDate", DetainedLicensesDTO.DetainDate );

	command.Parameters.AddWithValue("@FineFees", DetainedLicensesDTO.FineFees );

	command.Parameters.AddWithValue("@CreatedByUserID", DetainedLicensesDTO.CreatedByUserID );

	command.Parameters.AddWithValue("@IsReleased", DetainedLicensesDTO.IsReleased );

	command.Parameters.AddWithValue("@ReleaseDate", DetainedLicensesDTO.ReleaseDate );

	command.Parameters.AddWithValue("@ReleasedByUserID", DetainedLicensesDTO.ReleasedByUserID );

	command.Parameters.AddWithValue("@ReleaseApplicationID", DetainedLicensesDTO.ReleaseApplicationID );



                var outputIdParam = new SqlParameter("@NewDetainedLicensesId", SqlDbType.Int)
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


        public static bool UpdateDetainedLicenses(DetainedLicensesDTO DetainedLicensesDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_UpdateDetainedLicenses]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters with null checks
                        command.Parameters.AddWithValue("@DetainID", DetainedLicensesDTO.DetainID);
                        command.Parameters.AddWithValue("@LicenseID", DetainedLicensesDTO.LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DetainedLicensesDTO.DetainDate);
                        command.Parameters.AddWithValue("@FineFees", DetainedLicensesDTO.FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", DetainedLicensesDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@IsReleased", DetainedLicensesDTO.IsReleased);

                        // Handling nullable ReleaseDate
                        if (DetainedLicensesDTO.ReleaseDate.HasValue)
                            command.Parameters.AddWithValue("@ReleaseDate", DetainedLicensesDTO.ReleaseDate.Value);
                        else
                            command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

                        // Handling nullable ReleasedByUserID
                        if (DetainedLicensesDTO.ReleasedByUserID.HasValue)
                            command.Parameters.AddWithValue("@ReleasedByUserID", DetainedLicensesDTO.ReleasedByUserID.Value);
                        else
                            command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

                        // Handling nullable ReleaseApplicationID
                        if (DetainedLicensesDTO.ReleaseApplicationID.HasValue)
                            command.Parameters.AddWithValue("@ReleaseApplicationID", DetainedLicensesDTO.ReleaseApplicationID.Value);
                        else
                            command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

                        // Execute the query
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

        public static bool DeleteDetainedLicenses(int DetainID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteDetainedLicenses]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@DetainID", DetainID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

        public static async Task<bool> IsDetainedLicensesExist(int detainID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsDetainedLicensesExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainID; // تحديد نوع البيانات بشكل صريح

                        await connection.OpenAsync(); // استخدام الفتح غير المتزامن

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // تحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error checking existence of detained license with ID {detainID}: {ex}");
            }

            return isFound; // إرجاع النتيجة
        }




        public static List<DetainedLicensesDTO> GetAllDetainedLicenses()
{
    var DetainedLicensesList = new List<DetainedLicensesDTO>();
    
    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "[SP_GetAllDetainedLicenses]";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Handling nullable fields using DBNull check
                        int? releasedByUserID = reader.IsDBNull(reader.GetOrdinal("ReleasedByUserID")) 
                            ? (int?)null 
                            : reader.GetInt32(reader.GetOrdinal("ReleasedByUserID"));

                        DateTime? releaseDate = reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) 
                            ? (DateTime?)null 
                            : reader.GetDateTime(reader.GetOrdinal("ReleaseDate"));

                        int? releaseApplicationID = reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID")) 
                            ? (int?)null 
                            : reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID"));

                        DetainedLicensesList.Add(new DetainedLicensesDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("DetainID")),
                            reader.GetInt32(reader.GetOrdinal("LicenseID")),
                            reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                            reader.GetDecimal(reader.GetOrdinal("FineFees")),
                            reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                            reader.GetBoolean(reader.GetOrdinal("IsReleased")),
                            releaseDate, // Nullable DateTime
                            releasedByUserID, // Nullable int
                            releaseApplicationID // Nullable int
                        ));
                    }

                    reader.Close();
                }
            }
        }
    }
    catch (Exception ex) 
    {
        clsErrorHandling.HandleError(ex.ToString());
    }

    return DetainedLicensesList;
}



}

}