using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{


public class PeopleDTO
{
public int PersonID {get; set;}
public string NationalNo {get; set;}
public string FirstName {get; set;}
public string SecondName {get; set;}
public string ThirdName {get; set;}
public string LastName {get; set;}
public DateTime DateOfBirth {get; set;}
public byte Gendor {get; set;}
public string Address {get; set;}
public string Phone {get; set;}
public string Email {get; set;}
public int NationalityCountryID {get; set;}
public string ImagePath {get; set;}


public PeopleDTO ( int PersonID,  string NationalNo,  string FirstName,  string SecondName,  string ThirdName,  string LastName,  DateTime DateOfBirth,  byte Gendor,  string Address,  string Phone,  string Email,  int NationalityCountryID,  string ImagePath)
{
this.PersonID = PersonID;
this.NationalNo = NationalNo;
this.FirstName = FirstName;
this.SecondName = SecondName;
this.ThirdName = ThirdName;
this.LastName = LastName;
this.DateOfBirth = DateOfBirth;
this.Gendor = Gendor;
this.Address = Address;
this.Phone = Phone;
this.Email = Email;
this.NationalityCountryID = NationalityCountryID;
this.ImagePath = ImagePath;


}


}


public static class clsPeopleDataAccess
{
public static PeopleDTO GetPeopleInfoByID(int PersonID)
{
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
{
	string query = "[SP_GetPeopleInfoByID]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;

	command.Parameters.AddWithValue("@PersonID", PersonID);

		connection.Open();
		using(SqlDataReader reader = command.ExecuteReader())
{

		if (reader.Read())
		{
			 return new PeopleDTO

(
			reader.GetInt32(reader.GetOrdinal("PersonID")),		
			reader.GetString(reader.GetOrdinal("NationalNo")),	
			reader.GetString(reader.GetOrdinal("FirstName")),		
			reader.GetString(reader.GetOrdinal("SecondName")),
             reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : reader.GetString(reader.GetOrdinal("ThirdName")),
            reader.GetString(reader.GetOrdinal("LastName")),		
			reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),	
			reader.GetByte(reader.GetOrdinal("Gendor")),		
			reader.GetString(reader.GetOrdinal("Address")),		
			reader.GetString(reader.GetOrdinal("Phone")),
  reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
            reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
            // التحقق مما إذا كانت القيمة null قبل القراءة
            reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
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
public static int AddPeople(PeopleDTO PeopleDTO) {

int ID = -1;
   try {
    using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
{
        string query = "[SP_AddPeople]";

    using (	SqlCommand command = new SqlCommand(query, connection))
        {
   command.CommandType = CommandType.StoredProcedure;

        
	command.Parameters.AddWithValue("@NationalNo", PeopleDTO.NationalNo );

	command.Parameters.AddWithValue("@FirstName", PeopleDTO.FirstName );

	command.Parameters.AddWithValue("@SecondName", PeopleDTO.SecondName );

	command.Parameters.AddWithValue("@ThirdName", PeopleDTO.ThirdName );

	command.Parameters.AddWithValue("@LastName", PeopleDTO.LastName );

	command.Parameters.AddWithValue("@DateOfBirth", PeopleDTO.DateOfBirth );

	command.Parameters.AddWithValue("@Gendor", PeopleDTO.Gendor );

	command.Parameters.AddWithValue("@Address", PeopleDTO.Address );

	command.Parameters.AddWithValue("@Phone", PeopleDTO.Phone );

	command.Parameters.AddWithValue("@Email", PeopleDTO.Email );

	command.Parameters.AddWithValue("@NationalityCountryID", PeopleDTO.NationalityCountryID );

	command.Parameters.AddWithValue("@ImagePath", PeopleDTO.ImagePath );



                var outputIdParam = new SqlParameter("@NewPeopleId", SqlDbType.Int)
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


public static bool UpdatePeople(PeopleDTO PeopleDTO )
{
int rowsAffected=0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

{
	string query = "[SP_UpdatePeople]";
	using(	SqlCommand command = new SqlCommand(query, connection)) 
		{
	command.CommandType = CommandType.StoredProcedure;



	command.Parameters.AddWithValue("@PersonID", PeopleDTO.PersonID );

	command.Parameters.AddWithValue("@NationalNo", PeopleDTO.NationalNo );

	command.Parameters.AddWithValue("@FirstName", PeopleDTO.FirstName );

	command.Parameters.AddWithValue("@SecondName", PeopleDTO.SecondName );

	command.Parameters.AddWithValue("@ThirdName", PeopleDTO.ThirdName );

	command.Parameters.AddWithValue("@LastName", PeopleDTO.LastName );

	command.Parameters.AddWithValue("@DateOfBirth", PeopleDTO.DateOfBirth );

	command.Parameters.AddWithValue("@Gendor", PeopleDTO.Gendor );

	command.Parameters.AddWithValue("@Address", PeopleDTO.Address );

	command.Parameters.AddWithValue("@Phone", PeopleDTO.Phone );

	command.Parameters.AddWithValue("@Email", PeopleDTO.Email );

	command.Parameters.AddWithValue("@NationalityCountryID", PeopleDTO.NationalityCountryID );

	command.Parameters.AddWithValue("@ImagePath", PeopleDTO.ImagePath );


		connection.Open(); rowsAffected = command.ExecuteNonQuery();		}
		}
		}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected > 0);

}
public static bool DeletePeople(int PersonID)
{
	int rowsAffected = 0;
try{
using(	SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

{
	string query = "[SP_DeletePeople]";
using(	SqlCommand command = new SqlCommand(query, connection)) 
{
	command.CommandType = CommandType.StoredProcedure;


	command.Parameters.AddWithValue("@PersonID", PersonID );

		connection.Open();
		rowsAffected = (int)command.ExecuteNonQuery();
}
}
}

	catch (Exception ex) {clsErrorHandling.HandleError(ex.ToString());}

	return (rowsAffected == 1);


}

        public static async Task<bool> IsPeopleExistAsync(int personID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsPeopleExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;

                        await connection.OpenAsync();

                        // استخدام ExecuteScalar للحصول على عدد الصفوف
                        var result = await command.ExecuteScalarAsync();
                        isFound = (result != null && (int)result > 0); // التحقق مما إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError($"Error checking existence of person with ID {personID}: {ex}");
            }

            return isFound;
        }



        public static List<PeopleDTO> GetAllPeople()
        {
            var PeopleList = new List<PeopleDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    // تحديد الأعمدة التي تحتاجها بدلاً من استخدام *
                    string query = "[SP_GetGetAllPeople]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PeopleList.Add(new PeopleDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("NationalNo")),
                                    reader.GetString(reader.GetOrdinal("FirstName")),
                                    reader.GetString(reader.GetOrdinal("SecondName")),
                                  reader.IsDBNull(reader.GetOrdinal("ThirdName"))?null:  reader.GetString(reader.GetOrdinal("ThirdName")),
                                    reader.GetString(reader.GetOrdinal("LastName")),
                                    reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    reader.GetByte(reader.GetOrdinal("Gendor")),
                                    reader.GetString(reader.GetOrdinal("Address")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                  reader.IsDBNull(reader.GetOrdinal("Email"))?null:  reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                      // التحقق مما إذا كانت القيمة null قبل القراءة
                                    reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))));
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

            return PeopleList;
        }



    }

}