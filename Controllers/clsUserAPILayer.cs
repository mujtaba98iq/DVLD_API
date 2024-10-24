using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersBusinessLayer;
using MyDVLDDataAccessLayer;
namespace UsersApi.Controllers
{

[ApiController]

[Route("api/Users")]

public class UsersController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllUsers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<UsersDTO>> GetAllUsers()
{

List<UsersDTO> UsersList = clsUsers.GetAllUsers();

 if (UsersList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(UsersList);

}


[HttpGet("GetByID/{id}", Name = "GetUsersById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<UsersDTO> GetUsersById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsUsers Users = clsUsers.Find(id);

 if (Users == null)

{

 return NotFound("Not Found");
}

UsersDTO UDTO = Users.UserDTO;

 return Ok(UDTO);

}


[HttpPost("Add",Name = "AddUsers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<UsersDTO> AddUsers(UsersDTO newUsersDTO)
{

 if (newUsersDTO == null || newUsersDTO.UserID < 1 ||
newUsersDTO.PersonID < 0 ||
string.IsNullOrEmpty(newUsersDTO.UserName) ||
string.IsNullOrEmpty(newUsersDTO.Password) 
)


{

 return BadRequest("Invalid Data");
}

UsersBusinessLayer.clsUsers Users = new UsersBusinessLayer.clsUsers( new UsersDTO(newUsersDTO.UserID,newUsersDTO.PersonID,newUsersDTO.UserName,newUsersDTO.Password,newUsersDTO.IsActive ) ) ;

Users.Save();
newUsersDTO.UserID = Users.UserID;
return CreatedAtRoute("GetUsersById", new { id = newUsersDTO.UserID }, newUsersDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateUsers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<UsersDTO> UpdateUsers(int id,UsersDTO updatedUsersDTO)
{

 if (id < 1 || updatedUsersDTO == null || updatedUsersDTO.UserID < 1 ||
updatedUsersDTO.PersonID < 0 ||
string.IsNullOrEmpty(updatedUsersDTO.UserName) ||
string.IsNullOrEmpty(updatedUsersDTO.Password) 
)


{

 return BadRequest("Invalid Data");
}

 UsersBusinessLayer.clsUsers Users = clsUsers.Find(id);

 if (Users == null)

{

return NotFound($"Users with ID {id} not found.");

}

Users.UserID = updatedUsersDTO.UserID;
Users.PersonID = updatedUsersDTO.PersonID;
Users.UserName = updatedUsersDTO.UserName;
Users.Password = updatedUsersDTO.Password;
Users.IsActive = updatedUsersDTO.IsActive;

Users.Save();
 return Ok(Users.UserDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteUsers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteUsers(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(UsersBusinessLayer.clsUsers.DeleteUsers(id))

  return Ok($"Users with ID {id} has been deleted.");

else

return NotFound($"Users with ID {id} not found. no rows deleted!");

}
        [HttpGet("IsExistByID/{id}", Name = "IsUserExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult> IsUserExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid User ID. Please provide a valid ID.");
            }

            bool exists = await clsUsers.isUserExist(id);

            if (exists)
            {
                return Ok($"User with ID {id} exists.");
            }
            else
            {
                return NotFound($"User with ID {id} does not exist.");
            }
        }



    }

}