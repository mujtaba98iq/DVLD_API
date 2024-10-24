using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class DriversDTO
{
public int DriverID {get; set;}
public int PersonID {get; set;}
public int CreatedByUserID {get; set;}
public DateTime CreatedDate {get; set;}


public DriversDTO ( int DriverID,  int PersonID,  int CreatedByUserID,  DateTime CreatedDate)
{
this.DriverID = DriverID;
this.PersonID = PersonID;
this.CreatedByUserID = CreatedByUserID;
this.CreatedDate = CreatedDate;


}


}


public static class clsDriversDataAccess
{
public static DriversDTO GetDriversInfoByID(int DriverID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "SP_GetDriversInfoByID";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@DriverID", DriverID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new DriversDTO

(
			reader.GetInt32(reader.GetOrdinal("DriverID")),	
			reader.GetInt32(reader.GetOrdinal("PersonID")),		
			reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),	
			reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
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
        public static int AddDrivers(DriversDTO DriversDTO)
        {
            int ID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SP_AddDrivers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int)).Value = DriversDTO.PersonID;
                        command.Parameters.Add(new SqlParameter("@CreatedByUserID", SqlDbType.Int)).Value = DriversDTO.CreatedByUserID;

                        // استخدام smalldatetime هنا
                        command.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.SmallDateTime)).Value = DriversDTO.CreatedDate;

                        var outputIdParam = new SqlParameter("@NewDriverId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // التحقق من نجاح العملية قبل تعيين ID
                        if (rowsAffected > 0 && outputIdParam.Value != DBNull.Value)
                        {
                            ID = (int)outputIdParam.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return ID;
        }




        public static bool UpdateDrivers(DriversDTO DriversDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SP_UpdateDrivers";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // تعيين المعاملات بشكل صحيح
                        command.Parameters.Add(new SqlParameter("@DriverID", SqlDbType.Int)).Value = DriversDTO.DriverID;
                        command.Parameters.Add(new SqlParameter("@PersonID", SqlDbType.Int)).Value = DriversDTO.PersonID;
                        command.Parameters.Add(new SqlParameter("@CreatedByUserID", SqlDbType.Int)).Value = DriversDTO.CreatedByUserID;

                        // تعيين CreatedDate كـ smalldatetime
                        command.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.SmallDateTime)).Value = DriversDTO.CreatedDate;

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

        public static bool DeleteDrivers(int DriverID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "SP_DeleteDrivers";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.Add(new SqlParameter("@DriverID", SqlDbType.Int)).Value = DriverID;


		connection.Open();
		rowsAffected = (int)command.ExecuteScalar();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}



public static List<DriversDTO> GetAllDrivers()
{

 var DriversList = new List<DriversDTO>();
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
	string query = "SP_GetAllDrivers";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader()){
		while (reader.Read())
		{
		DriversList.Add(new DriversDTO
				(
			reader.GetInt32(reader.GetOrdinal("DriverID")),
			reader.GetInt32(reader.GetOrdinal("PersonID")),	
			reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),	
			reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
				));
		}
		reader.Close();
}
	}	}	}
	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return DriversList;
}


}

}