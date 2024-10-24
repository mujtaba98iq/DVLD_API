using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class TestTypesDTO
{
public int TestTypeID {get; set;}
public string TestTypeTitle {get; set;}
public string TestTypeDescription {get; set;}
public decimal TestFees {get; set;}


public TestTypesDTO ( int TestTypeID,  string TestTypeTitle,  string TestTypeDescription,  decimal TestFees)
{
this.TestTypeID = TestTypeID;
this.TestTypeTitle = TestTypeTitle;
this.TestTypeDescription = TestTypeDescription;
this.TestFees = TestFees;


}


}


public static class clsTestTypesDataAccess
{
public static TestTypesDTO GetTestTypesInfoByID(int TestTypeID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetTestTypesInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new TestTypesDTO

(
			reader.GetInt32(reader.GetOrdinal("TestTypeID")),			reader.GetString(reader.GetOrdinal("TestTypeTitle")),			reader.GetString(reader.GetOrdinal("TestTypeDescription")),			reader.GetDecimal(reader.GetOrdinal("TestFees"))
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
public static int AddTestTypes(TestTypesDTO TestTypesDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = @"[SP_AddTestTypes]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@TestTypeTitle", TestTypesDTO.TestTypeTitle );

	command.Parameters.AddWithValue("@TestTypeDescription", TestTypesDTO.TestTypeDescription );

	command.Parameters.AddWithValue("@TestFees", TestTypesDTO.TestFees );



                var outputIdParam = new SqlParameter("@NewTestTypesId", SqlDbType.Int)
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


public static bool UpdateTestTypes(TestTypesDTO TestTypesDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = @"[SP_UpdateTestTypes]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@TestTypeID", TestTypesDTO.TestTypeID );

	command.Parameters.AddWithValue("@TestTypeTitle", TestTypesDTO.TestTypeTitle );

	command.Parameters.AddWithValue("@TestTypeDescription", TestTypesDTO.TestTypeDescription );

	command.Parameters.AddWithValue("@TestFees", TestTypesDTO.TestFees );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeleteTestTypes(int TestTypeID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeleteTestTypes]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@TestTypeID", TestTypeID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

        public static async Task<bool> IsTestTypesExistAsync(int testTypeID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsTestTypesExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تعيين نوع الأمر إلى إجراء مخزن
                        command.CommandType = CommandType.StoredProcedure;

                        // إضافة البراميتر
                        command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID; // تحديد نوع البيانات بشكل صريح

                        await connection.OpenAsync(); // استخدام الفتح غير المتزامن

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // تحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error checking existence of test type with ID {testTypeID}: {ex}");
            }

            return isFound; // إرجاع النتيجة
        }


        public static List<TestTypesDTO> GetAllTestTypes()
        {
            var TestTypesList = new List<TestTypesDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    // تحديد الأعمدة المطلوبة فقط في الاستعلام
                    string query = "[SP_GetAllTestTypes]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // قراءة الأعمدة المحددة فقط
                                int testTypeID = reader.GetInt32(reader.GetOrdinal("TestTypeID"));
                                string testTypeTitle = reader.GetString(reader.GetOrdinal("TestTypeTitle"));
                                string testTypeDescription = reader.GetString(reader.GetOrdinal("TestTypeDescription"));
                                decimal testFees = reader.GetDecimal(reader.GetOrdinal("TestFees"));

                                // إضافة البيانات إلى القائمة
                                TestTypesList.Add(new TestTypesDTO
                                (
                                    testTypeID,
                                    testTypeTitle,
                                    testTypeDescription,
                                    testFees
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

            return TestTypesList;
        }


    }

}