using System;
using System.Data;
using D.L_DVLD_API;
using Microsoft.Data.SqlClient;

namespace MyDVLDDataAccessLayer
{
public class LicensesDTO
{
public int LicenseID {get; set;}
public int ApplicationID {get; set;}
public int DriverID {get; set;}
public int LicenseClass {get; set;}
public DateTime IssueDate {get; set;}
public DateTime ExpirationDate {get; set;}
public string Notes {get; set;}
public decimal PaidFees {get; set;}
public bool IsActive {get; set;}
public byte IssueReason {get; set;}
public int CreatedByUserID {get; set;}


public LicensesDTO ( int LicenseID,  int ApplicationID,  int DriverID,  int LicenseClass,  DateTime IssueDate,  DateTime ExpirationDate,  string Notes,  decimal PaidFees,  bool IsActive,  byte IssueReason,  int CreatedByUserID)
{
this.LicenseID = LicenseID;
this.ApplicationID = ApplicationID;
this.DriverID = DriverID;
this.LicenseClass = LicenseClass;
this.IssueDate = IssueDate;
this.ExpirationDate = ExpirationDate;
this.Notes = Notes;
this.PaidFees = PaidFees;
this.IsActive = IsActive;
this.IssueReason = IssueReason;
this.CreatedByUserID = CreatedByUserID;


}


}


public static class clsLicensesDataAccess
{
        public static LicensesDTO GetLicensesInfoByID(int LicenseID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_GetLicensesInfoByID]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new LicensesDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                    reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                    reader.GetInt32(reader.GetOrdinal("DriverID")),
                                    reader.GetInt32(reader.GetOrdinal("LicenseClass")),
                                    reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                    reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                    reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.GetByte(reader.GetOrdinal("IssueReason")),
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
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return null;
        }

        public static int AddLicenses(LicensesDTO LicensesDTO)
        {
            int ID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_AddLicenses]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ApplicationID", LicensesDTO.ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", LicensesDTO.DriverID);
                        command.Parameters.AddWithValue("@LicenseClass", LicensesDTO.LicenseClass);
                        command.Parameters.AddWithValue("@IssueDate", LicensesDTO.IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", LicensesDTO.ExpirationDate);
                        command.Parameters.AddWithValue("@Notes", LicensesDTO.Notes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PaidFees", LicensesDTO.PaidFees);
                        command.Parameters.AddWithValue("@IsActive", LicensesDTO.IsActive);
                        command.Parameters.AddWithValue("@IssueReason", LicensesDTO.IssueReason);
                        command.Parameters.AddWithValue("@CreatedByUserID", LicensesDTO.CreatedByUserID);

                        var outputIdParam = new SqlParameter("@NewLicensesId", SqlDbType.Int)
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



        public static bool UpdateLicenses(LicensesDTO LicensesDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"[SP_UpdateLicenses]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@LicenseID", LicensesDTO.LicenseID);
                        command.Parameters.AddWithValue("@ApplicationID", LicensesDTO.ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", LicensesDTO.DriverID);
                        command.Parameters.AddWithValue("@LicenseClass", LicensesDTO.LicenseClass);
                        command.Parameters.AddWithValue("@IssueDate", LicensesDTO.IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", LicensesDTO.ExpirationDate);
                        command.Parameters.AddWithValue("@Notes", LicensesDTO.Notes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PaidFees", LicensesDTO.PaidFees);
                        command.Parameters.AddWithValue("@IsActive", LicensesDTO.IsActive);
                        command.Parameters.AddWithValue("@IssueReason", LicensesDTO.IssueReason);
                        command.Parameters.AddWithValue("@CreatedByUserID", LicensesDTO.CreatedByUserID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                clsErrorHandling.HandleError(sqlEx.ToString());
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return rowsAffected > 0;
        }

        public static bool DeleteLicenses(int LicenseID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_DeleteLicenses]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery(); // استخدام ExecuteNonQuery بدلاً من ExecuteScalar
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }
            return (rowsAffected > 0); // إرجاع true إذا تم الحذف بنجاح
        }


        public static bool IsLicensesExist(int LicenseID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_IsLicensesExist]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        // استخدام ExecuteScalar للحصول على النتيجة بشكل مباشر
                        var result = command.ExecuteScalar();
                        isFound = (result != null && (int)result > 0); // تحقق إذا كانت النتيجة أكبر من 0
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return isFound;
        }


        public static List<LicensesDTO> GetAllLicenses()
        {
            var licensesList = new List<LicensesDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "[SP_GetAllLicenses]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                licensesList.Add(new LicensesDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("LicenseID")),
                                    reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                    reader.GetInt32(reader.GetOrdinal("DriverID")),
                                    reader.GetInt32(reader.GetOrdinal("LicenseClass")),
                                    reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                                    reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                    reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.GetByte(reader.GetOrdinal("IssueReason")),
                                    reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                clsErrorHandling.HandleError(sqlEx.ToString());
            }
            catch (Exception ex)
            {
                clsErrorHandling.HandleError(ex.ToString());
            }

            return licensesList;
        }



    }

}