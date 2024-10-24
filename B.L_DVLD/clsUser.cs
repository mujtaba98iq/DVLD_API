using System;
using System.Data;
using MyDVLDDataAccessLayer;
namespace UsersBusinessLayer
{

public class clsUsers
{
 public UsersDTO UserDTO
  {get { return (new UsersDTO(this.UserID,this.PersonID,this.UserName,this.Password,this.IsActive)); }}
public enum enMode { AddNew = 0, Update = 1 };
public enMode Mode = enMode.AddNew;
public int UserID {get; set;}
public int PersonID {get; set;}
public string UserName {get; set;}
public string Password {get; set;}
public bool IsActive {get; set;}


public clsUsers()
{
this.UserID = default;
this.PersonID = default;
this.UserName = default;
this.Password = default;
this.IsActive = default;


Mode = enMode.AddNew;

}

public clsUsers(UsersDTO UDTO, enMode cMode =enMode.AddNew) 
{
this.UserID = UDTO.UserID;
this.PersonID = UDTO.PersonID;
this.UserName = UDTO.UserName;
this.Password = UDTO.Password;
this.IsActive = UDTO.IsActive;


Mode = cMode;

}

private bool _AddNewUsers()
{
this.UserID = clsUsersDataAccess.AddUsers(UserDTO);
return (this.UserID != -1);

}

private bool _UpdateUsers()
{
return clsUsersDataAccess.UpdateUsers(UserDTO);
}

public static clsUsers Find(int Id)
{
UsersDTO UDTO = clsUsersDataAccess.GetUsersInfoByID(Id);

if(UDTO != null)
return new clsUsers(UDTO,enMode.Update);
else
return null;

}

        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUsers())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUsers();

            }




            return false;
        }

public static List<UsersDTO> GetAllUsers(){return clsUsersDataAccess.GetAllUsers();}

public static bool DeleteUsers(int UserID){return  clsUsersDataAccess.DeleteUsers(UserID);}

public static async Task< bool> isUserExist(int UserID){return await clsUsersDataAccess.IsUsersExistAsync(UserID);}


}

}