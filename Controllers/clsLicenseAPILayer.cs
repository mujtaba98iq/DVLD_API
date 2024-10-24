using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LicensesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace LicensesApi.Controllers
{

[ApiController]

[Route("api/Licenses")]

public class LicensesController: ControllerBase
{
        [HttpGet("GetAll",Name = "GetAllLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LicensesDTO>> GetAllLicenses()
        {
            var licensesList = clsLicenses.GetAllLicenses();

            if (!licensesList.Any())
            {
                return NotFound("No licenses found.");
            }

            return Ok(licensesList);
        }



        [HttpGet("GetByID/{id}", Name = "GetLicensesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<LicensesDTO> GetLicensesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsLicenses Licenses = clsLicenses.Find(id);

 if (Licenses == null)

{

 return NotFound("Not Found");
}

LicensesDTO LDTO = Licenses.LicensDTO;

 return Ok(LDTO);

}


[HttpPost("Add",Name = "AddLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<LicensesDTO> AddLicenses(LicensesDTO newLicensesDTO)
{

 if (newLicensesDTO == null || newLicensesDTO.LicenseID < 1||
newLicensesDTO.ApplicationID < 1||
newLicensesDTO.DriverID < 1 ||
newLicensesDTO.LicenseClass < 1||
string.IsNullOrEmpty(newLicensesDTO.Notes) ||
newLicensesDTO.PaidFees < 0 ||
newLicensesDTO.IssueReason < 0 ||
newLicensesDTO.CreatedByUserID < 1
)


{

 return BadRequest("Invalid Data");
}

LicensesBusinessLayer.clsLicenses Licenses = new LicensesBusinessLayer.clsLicenses( new LicensesDTO(newLicensesDTO.LicenseID,newLicensesDTO.ApplicationID,newLicensesDTO.DriverID,newLicensesDTO.LicenseClass,newLicensesDTO.IssueDate,newLicensesDTO.ExpirationDate,newLicensesDTO.Notes,newLicensesDTO.PaidFees,newLicensesDTO.IsActive,newLicensesDTO.IssueReason,newLicensesDTO.CreatedByUserID ) ) ;

Licenses.Save();
newLicensesDTO.LicenseID = Licenses.LicenseID;
return CreatedAtRoute("GetLicensesById", new { Id = newLicensesDTO.LicenseID }, newLicensesDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<LicensesDTO> UpdateLicenses(int id,LicensesDTO updatedLicensesDTO)
{

 if (id < 1 || updatedLicensesDTO == null || updatedLicensesDTO.LicenseID <1 ||
updatedLicensesDTO.ApplicationID < 1 ||
updatedLicensesDTO.DriverID < 1||
updatedLicensesDTO.LicenseClass < 1 ||
string.IsNullOrEmpty(updatedLicensesDTO.Notes) ||
updatedLicensesDTO.PaidFees < 0 ||
updatedLicensesDTO.IssueReason < 0 ||
updatedLicensesDTO.CreatedByUserID < 1
)


{

 return BadRequest("Invalid Data");
}

 LicensesBusinessLayer.clsLicenses Licenses = clsLicenses.Find(id);

 if (Licenses == null)

{

return NotFound($"Licenses with ID {id} not found.");

}

Licenses.LicenseID = updatedLicensesDTO.LicenseID;
Licenses.ApplicationID = updatedLicensesDTO.ApplicationID;
Licenses.DriverID = updatedLicensesDTO.DriverID;
Licenses.LicenseClass = updatedLicensesDTO.LicenseClass;
Licenses.IssueDate = updatedLicensesDTO.IssueDate;
Licenses.ExpirationDate = updatedLicensesDTO.ExpirationDate;
Licenses.Notes = updatedLicensesDTO.Notes;
Licenses.PaidFees = updatedLicensesDTO.PaidFees;
Licenses.IsActive = updatedLicensesDTO.IsActive;
Licenses.IssueReason = updatedLicensesDTO.IssueReason;
            Licenses.CreatedByUserID = updatedLicensesDTO.CreatedByUserID;

Licenses.Save();
 return Ok(Licenses.LicensDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteLicenses(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(LicensesBusinessLayer.clsLicenses.DeleteLicenses(id))

  return Ok($"Licenses with ID {id} has been deleted.");

else

return NotFound($"Licenses with ID {id} not found. no rows deleted!");

}
        [HttpGet("IsExistByID/{id}", Name = "IsLicensesExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsLicensesExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid License ID. Please provide a valid ID.");
            }

            bool exists = LicensesBusinessLayer.clsLicenses.isLicenseExist(id);

            if (exists)
            {
                return Ok($"License with ID {id} exists.");
            }
            else
            {
                return NotFound($"License with ID {id} does not exist.");
            }
        }



    }

}