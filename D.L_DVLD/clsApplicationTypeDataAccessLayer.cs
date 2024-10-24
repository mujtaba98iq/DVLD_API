using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class ApplcatoinTypesDTO
{
public int ApplicationTypeID {get; set;}
public string ApplicationTypeTitle {get; set;}
public decimal ApplicationFees {get; set;}


public ApplcatoinTypesDTO ( int ApplicationTypeID,  string ApplicationTypeTitle,  decimal ApplicationFees)
{
this.ApplicationTypeID = ApplicationTypeID;
this.ApplicationTypeTitle = ApplicationTypeTitle;
this.ApplicationFees = ApplicationFees;


}


}


public static class clsApplcatoinTypesDataAccess
{
public static ApplcatoinTypesDTO GetApplcatoinTypesInfoByID(int ApplicationTypeID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetApplcatoinTypesInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new ApplcatoinTypesDTO

(
			reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),			reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),			reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
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
public static int AddApplcatoinTypes(ApplcatoinTypesDTO ApplcatoinTypesDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = "[SP_AddApplcatoinTypes]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplcatoinTypesDTO.ApplicationTypeTitle );

	command.Parameters.AddWithValue("@ApplicationFees", ApplcatoinTypesDTO.ApplicationFees );



                var outputIdParam = new SqlParameter("@NewApplcatoinTypesId", SqlDbType.Int)
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


public static bool UpdateApplcatoinTypes(ApplcatoinTypesDTO ApplcatoinTypesDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"UPDATE ApplcatoinTypes
	SET	ApplicationTypeTitle = @ApplicationTypeTitle,
	ApplicationFees = @ApplicationFees	WHERE ApplicationTypeID = @ApplicationTypeID";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@ApplicationTypeID", ApplcatoinTypesDTO.ApplicationTypeID );

	command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplcatoinTypesDTO.ApplicationTypeTitle );

	command.Parameters.AddWithValue("@ApplicationFees", ApplcatoinTypesDTO.ApplicationFees );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteApplcatoinTypes(int ApplicationTypeID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteApplcatoinTypes]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}
        public static async Task<bool> IsApplicationTypesExistAsync(int applicationTypeID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsApplcatoinTypesExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeID; // تحديد نوع البيانات بشكل صريح

                        await connection.OpenAsync(); // استخدام الفتح غير المتزامن

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // التحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error checking existence of application type with ID {applicationTypeID}: {ex}");
            }

            return isFound; // إرجاع النتيجة
        }


        public static List<ApplcatoinTypesDTO> GetAllApplcatoinTypes()
{

 var ApplcatoinTypesList = new List<ApplcatoinTypesDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "[SP_GetAllApplcatoinTypes]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		ApplcatoinTypesList.Add(new ApplcatoinTypesDTO
				(
			reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),			reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),			reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return ApplcatoinTypesList;
}


}

}