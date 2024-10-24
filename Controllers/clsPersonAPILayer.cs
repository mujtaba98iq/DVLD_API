using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleBusinessLayer;
using MyDVLDDataAccessLayer;
namespace PeopleApi.Controllers
{

[ApiController]

[Route("api/People")]

public class PeopleController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllPeople")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<PeopleDTO>> GetAllPeople()
{

List<PeopleDTO> PeopleList = clsPeople.GetAllPeople();

 if (PeopleList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(PeopleList);

}


[HttpGet("GetByID/{id}", Name = "GetPeopleById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<PeopleDTO> GetPeopleById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsPeople People = clsPeople.Find(id);

 if (People == null)

{

 return NotFound("Not Found");
}

PeopleDTO PDTO = People.PersonDTO;

 return Ok(PDTO);

}


[HttpPost("Add",Name = "AddPeople")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<PeopleDTO> AddPeople(PeopleDTO newPeopleDTO)
{

 if (newPeopleDTO == null || newPeopleDTO.PersonID < 1 ||
string.IsNullOrEmpty(newPeopleDTO.NationalNo) ||
string.IsNullOrEmpty(newPeopleDTO.FirstName) ||
string.IsNullOrEmpty(newPeopleDTO.SecondName) ||
string.IsNullOrEmpty(newPeopleDTO.ThirdName) ||
string.IsNullOrEmpty(newPeopleDTO.LastName) ||
newPeopleDTO.Gendor < 0 ||
string.IsNullOrEmpty(newPeopleDTO.Address) ||
string.IsNullOrEmpty(newPeopleDTO.Phone) ||
string.IsNullOrEmpty(newPeopleDTO.Email) ||
newPeopleDTO.NationalityCountryID < 1 ||
string.IsNullOrEmpty(newPeopleDTO.ImagePath)
)


{

 return BadRequest("Invalid Data");
}

PeopleBusinessLayer.clsPeople People = new PeopleBusinessLayer.clsPeople( new PeopleDTO(newPeopleDTO.PersonID,newPeopleDTO.NationalNo,newPeopleDTO.FirstName,newPeopleDTO.SecondName,newPeopleDTO.ThirdName,newPeopleDTO.LastName,newPeopleDTO.DateOfBirth,newPeopleDTO.Gendor,newPeopleDTO.Address,newPeopleDTO.Phone,newPeopleDTO.Email,newPeopleDTO.NationalityCountryID,newPeopleDTO.ImagePath ) ) ;

People.Save();
newPeopleDTO.PersonID = People.PersonID;
return CreatedAtRoute("GetPeopleById", new { id = newPeopleDTO.PersonID }, newPeopleDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdatePeople")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<PeopleDTO> UpdatePeople(int id,PeopleDTO updatedPeopleDTO)
{

 if (id < 1 || updatedPeopleDTO == null || updatedPeopleDTO.PersonID < 1 ||
string.IsNullOrEmpty(updatedPeopleDTO.NationalNo) ||
string.IsNullOrEmpty(updatedPeopleDTO.FirstName) ||
string.IsNullOrEmpty(updatedPeopleDTO.SecondName) ||
string.IsNullOrEmpty(updatedPeopleDTO.ThirdName) ||
string.IsNullOrEmpty(updatedPeopleDTO.LastName) ||
updatedPeopleDTO.Gendor < 0 ||
string.IsNullOrEmpty(updatedPeopleDTO.Address) ||
string.IsNullOrEmpty(updatedPeopleDTO.Phone) ||
string.IsNullOrEmpty(updatedPeopleDTO.Email) ||
updatedPeopleDTO.NationalityCountryID < 1 ||
string.IsNullOrEmpty(updatedPeopleDTO.ImagePath)
)


{

 return BadRequest("Invalid Data");
}

 PeopleBusinessLayer.clsPeople People = clsPeople.Find(id);

 if (People == null)

{

return NotFound($"People with ID {id} not found.");

}

People.PersonID = updatedPeopleDTO.PersonID;
People.NationalNo = updatedPeopleDTO.NationalNo;
People.FirstName = updatedPeopleDTO.FirstName;
People.SecondName = updatedPeopleDTO.SecondName;
People.ThirdName = updatedPeopleDTO.ThirdName;
People.LastName = updatedPeopleDTO.LastName;
People.DateOfBirth = updatedPeopleDTO.DateOfBirth;
People.Gendor = updatedPeopleDTO.Gendor;
People.Address = updatedPeopleDTO.Address;
People.Phone = updatedPeopleDTO.Phone;
People.Email = updatedPeopleDTO.Email;
People.NationalityCountryID = updatedPeopleDTO.NationalityCountryID;
People.ImagePath = updatedPeopleDTO.ImagePath;

People.Save();
 return Ok(People.PersonDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeletePeople")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeletePeople(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(PeopleBusinessLayer.clsPeople.DeletePeople(id))

  return Ok($"People with ID {id} has been deleted.");

else

return NotFound($"People with ID {id} not found. no rows deleted!");

}

        [HttpGet("IsExistByID/{id}", Name = "IsPersonExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult> IsPersonExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid person ID. Please provide a valid ID.");
            }

            bool exists =await clsPeople.IsPersonExistAsync(id);

            if (exists)
            {
                return Ok($"person with ID {id} exists.");
            }
            else
            {
                return NotFound($"person with ID {id} does not exist.");
            }
        }



    }

}