using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class TestAppointmentDTO
{
      
            public int TestAppointmentID { get; set; }
            public int TestTypeID { get; set; }
            public int LocalDrivingLicenseApplicationID { get; set; }
            public DateTime AppointmentDate { get; set; }
            public decimal PaidFees { get; set; }
            public int CreatedByUserID { get; set; }
            public bool IsLocked { get; set; }
            public int? RetakeTestApplicationID { get; set; } // نوع Nullable
        


        public TestAppointmentDTO ( int TestAppointmentID,  int TestTypeID,  int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  decimal PaidFees,  int CreatedByUserID,  bool IsLocked,  int? RetakeTestApplicationID)
{
this.TestAppointmentID = TestAppointmentID;
this.TestTypeID = TestTypeID;
this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
this.AppointmentDate = AppointmentDate;
this.PaidFees = PaidFees;
this.CreatedByUserID = CreatedByUserID;
this.IsLocked = IsLocked;
this.RetakeTestApplicationID = RetakeTestApplicationID;


}


}


public static class clsTestAppointmentDataAccess
{
public static TestAppointmentDTO GetTestAppointmentInfoByID(int TestAppointmentID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetTestAppointmentInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new TestAppointmentDTO
(
			reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),	
            reader.GetInt32(reader.GetOrdinal("TestTypeID")),		
            reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),	
            reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),	
            reader.GetDecimal(reader.GetOrdinal("PaidFees")),		
            reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),	
            reader.GetBoolean(reader.GetOrdinal("IsLocked")),
             reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID"))
                                        ? (int?)null
                                        : reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID")) // التعامل مع القيم null
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
        public static int AddTestAppointment(TestAppointmentDTO TestAppointmentDTO)
        {
            int ID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_AddTestAppointment]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TestTypeID", TestAppointmentDTO.TestTypeID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", TestAppointmentDTO.LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@AppointmentDate", TestAppointmentDTO.AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", TestAppointmentDTO.PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", TestAppointmentDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@IsLocked", TestAppointmentDTO.IsLocked);

                        // التحقق إذا كانت RetakeTestApplicationID فارغة
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", (object)TestAppointmentDTO.RetakeTestApplicationID ?? DBNull.Value);


                        var outputIdParam = new SqlParameter("@NewTestAppointmentId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();
                        ID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return ID;
        }



        public static bool UpdateTestAppointment(TestAppointmentDTO TestAppointmentDTO)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"[SP_UpdateTestAppointment]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // إضافة المعلمات
                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentDTO.TestAppointmentID);
                        command.Parameters.AddWithValue("@TestTypeID", TestAppointmentDTO.TestTypeID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", TestAppointmentDTO.LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@AppointmentDate", TestAppointmentDTO.AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", TestAppointmentDTO.PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", TestAppointmentDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@IsLocked", TestAppointmentDTO.IsLocked);
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", (object)TestAppointmentDTO.RetakeTestApplicationID ?? DBNull.Value); // التعامل مع القيم NULL

                        // فتح الاتصال وتنفيذ الاستعلام
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                throw; // إعادة رفع الاستثناء بعد تسجيله
            }

            // إرجاع الحالة بناءً على عدد الصفوف المتأثرة
            return (rowsAffected > 0);
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteTestAppointment]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}
        public static async Task<bool> IsTestAppointmentExistAsync(int testAppointmentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsTestAppointmentExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تعيين نوع الأمر إلى إجراء مخزن
                        command.CommandType = CommandType.StoredProcedure;

                        // إضافة البراميتر
                        command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testAppointmentID; // تحديد نوع البيانات بشكل صريح

                        await connection.OpenAsync(); // استخدام الفتح غير المتزامن

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // تحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error checking existence of test appointment with ID {testAppointmentID}: {ex}");
            }

            return isFound; // إرجاع النتيجة
        }



        public static List<TestAppointmentDTO> GetAllTestAppointment()
        {
            var testAppointmentList = new List<TestAppointmentDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_GetAllTestAppointment]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text; // استخدام CommandType.Text هنا بدلاً من StoredProcedure لأننا نستخدم استعلام عادي

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                testAppointmentList.Add(new TestAppointmentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                    reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                    reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
                                    reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                    reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                    reader.GetBoolean(reader.GetOrdinal("IsLocked")),
                                    reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID"))
                                        ? (int?)null
                                        : reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID")) // التعامل مع القيم null
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

            return testAppointmentList;
        }


    }

}