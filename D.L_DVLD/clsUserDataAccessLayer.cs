using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class UsersDTO
{
public int UserID {get; set;}
public int PersonID {get; set;}
public string UserName {get; set;}
public string Password {get; set;}
public bool IsActive {get; set;}


public UsersDTO ( int UserID,  int PersonID,  string UserName,  string Password,  bool IsActive)
{
this.UserID = UserID;
this.PersonID = PersonID;
this.UserName = UserName;
this.Password = Password;
this.IsActive = IsActive;


}


}


public static class clsUsersDataAccess
{
        public static UsersDTO GetUsersInfoByID(int UserID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetUsersInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@UserID", UserID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new UsersDTO

(
			reader.GetInt32(reader.GetOrdinal("UserID")),			reader.GetInt32(reader.GetOrdinal("PersonID")),			reader.GetString(reader.GetOrdinal("UserName")),			reader.GetString(reader.GetOrdinal("Password")),			reader.GetBoolean(reader.GetOrdinal("IsActive"))
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
public static int AddUsers(UsersDTO UsersDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"SP_AddUsers";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@PersonID", UsersDTO.PersonID );

	command.Parameters.AddWithValue("@UserName", UsersDTO.UserName );

	command.Parameters.AddWithValue("@Password", UsersDTO.Password );

	command.Parameters.AddWithValue("@IsActive", UsersDTO.IsActive );



                var outputIdParam = new SqlParameter("@NewUsersId", SqlDbType.Int)
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


public static bool UpdateUsers(UsersDTO UsersDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"[SP_UpdateUsers]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@UserID", UsersDTO.UserID );

	command.Parameters.AddWithValue("@PersonID", UsersDTO.PersonID );

	command.Parameters.AddWithValue("@UserName", UsersDTO.UserName );

	command.Parameters.AddWithValue("@Password", UsersDTO.Password );

	command.Parameters.AddWithValue("@IsActive", UsersDTO.IsActive );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteUsers(int UserID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteUsers]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@UserID", UserID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

        public static async Task<bool> IsUsersExistAsync(int userID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsUsersExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; // تعيين نوع الأمر إلى إجراء مخزن
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID; // تحديد نوع البيانات بشكل صريح

                        await connection.OpenAsync(); // استخدام الفتح غير المتزامن

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // تحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error checking existence of user with ID {userID}: {ex}");
            }

            return isFound; // إرجاع النتيجة
        }


        public static List<UsersDTO> GetAllUsers()
        {
            var UsersList = new List<UsersDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    // تحديد الأعمدة المطلوبة بدلاً من SELECT *
                    string query = "[SP_GetAllUsers]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تعيين نوع الاستعلام كنص عادي
                        command.CommandType = CommandType.Text;

                        // فتح الاتصال
                        connection.Open();

                        // قراءة البيانات باستخدام SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsersList.Add(new UsersDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("UserName")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return UsersList;
        }


    }

}