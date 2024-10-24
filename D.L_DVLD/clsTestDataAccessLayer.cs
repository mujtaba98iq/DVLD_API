using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class TestsDTO
{
public int TestID {get; set;}
public int TestAppointmentID {get; set;}
public bool TestResult {get; set;}
public string Notes {get; set;}
public int CreatedByUserID {get; set;}


public TestsDTO ( int TestID,  int TestAppointmentID,  bool TestResult,  string Notes,  int CreatedByUserID)
{
this.TestID = TestID;
this.TestAppointmentID = TestAppointmentID;
this.TestResult = TestResult;
this.Notes = Notes;
this.CreatedByUserID = CreatedByUserID;


}


}


public static class clsTestsDataAccess
{
public static TestsDTO GetTestsInfoByID(int TestID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetTestsInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@TestID", TestID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new TestsDTO

(
			reader.GetInt32(reader.GetOrdinal("TestID")),	
            reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),	
            reader.GetBoolean(reader.GetOrdinal("TestResult")),
            reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
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
public static int AddTests(TestsDTO TestsDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"[SP_AddTests]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@TestAppointmentID", TestsDTO.TestAppointmentID );

	command.Parameters.AddWithValue("@TestResult", TestsDTO.TestResult );

                        if (string.IsNullOrEmpty(TestsDTO.Notes))
                        {
                            command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Notes", TestsDTO.Notes);
                        }

                        command.Parameters.AddWithValue("@CreatedByUserID", TestsDTO.CreatedByUserID );



                var outputIdParam = new SqlParameter("@NewTestsId", SqlDbType.Int)
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


public static bool UpdateTests(TestsDTO TestsDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"[SP_UpdateTests]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@TestID", TestsDTO.TestID );

	command.Parameters.AddWithValue("@TestAppointmentID", TestsDTO.TestAppointmentID );

	command.Parameters.AddWithValue("@TestResult", TestsDTO.TestResult );

                        if (string.IsNullOrEmpty(TestsDTO.Notes))
                        {
                            command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Notes", TestsDTO.Notes);
                        }


                        command.Parameters.AddWithValue("@CreatedByUserID", TestsDTO.CreatedByUserID );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
        public static bool DeleteTests(int testID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_DeleteTests]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TestID", testID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
                // يمكنك إضافة رسالة خطأ هنا إذا كنت بحاجة لذلك
            }

            // إعادة true إذا تم حذف صف واحد، وإلا false
            return rowsAffected == 1;
        }

        public static async Task<bool> IsTestsExistAsync(int testID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsTestsExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تعيين نوع الاستعلام كإجراء مخزن
                        command.CommandType = CommandType.StoredProcedure;

                        // إضافة البراميتر
                        command.Parameters.Add("@TestID", SqlDbType.Int).Value = testID; // تحديد نوع البيانات بشكل صريح

                        await connection.OpenAsync(); // استخدام الفتح غير المتزامن

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // تحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                // معالجة الخطأ
                clsErrorHandling.HandleError($"Error checking existence of test with ID {testID}: {ex}");
            }

            return isFound; // إرجاع النتيجة
        }



        public static List<TestsDTO> GetAllTests()
        {
            var TestsList = new List<TestsDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    // تحديد الأعمدة المطلوبة بدلاً من استخدام *
                    string query = "[SP_GetAllTests]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; // استخدام CommandType.Text لأننا نستخدم استعلام عادي

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TestsList.Add(new TestsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("TestID")),
                                    reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                                    reader.GetBoolean(reader.GetOrdinal("TestResult")),
                                    // التحقق مما إذا كانت القيمة null قبل القراءة
                                    reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
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

            return TestsList;
        }



    }

}