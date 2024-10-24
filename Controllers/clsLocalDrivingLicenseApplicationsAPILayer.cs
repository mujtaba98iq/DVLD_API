using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocalDrivingLicenseApplicationsBusinessLayer;
using MyDVLDDataAccessLayer;
namespace LocalDrivingLicenseApplicationsApi.Controllers
{

[ApiController]

[Route("api/LocalDrivingLicenseApplications")]

public class LocalDrivingLicenseApplicationsController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllLocalDrivingLicenseApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<LocalDrivingLicenseApplicationsDTO>> GetAllLocalDrivingLicenseApplications()
{

List<LocalDrivingLicenseApplicationsDTO> LocalDrivingLicenseApplicationsList = clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();

 if (LocalDrivingLicenseApplicationsList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(LocalDrivingLicenseApplicationsList);

}


[HttpGet("GetByID/{id}", Name = "GetLocalDrivingLicenseApplicationsById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<LocalDrivingLicenseApplicationsDTO> GetLocalDrivingLicenseApplicationsById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(id);

 if (LocalDrivingLicenseApplications == null)

{

 return NotFound("Not Found");
}

LocalDrivingLicenseApplicationsDTO LDTO = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationDTO;

 return Ok(LDTO);

}


[HttpPost("Add",Name = "AddLocalDrivingLicenseApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<LocalDrivingLicenseApplicationsDTO> AddLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationsDTO newLocalDrivingLicenseApplicationsDTO)
{

 if (newLocalDrivingLicenseApplicationsDTO == null || newLocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID < 0 ||
newLocalDrivingLicenseApplicationsDTO.ApplicationID < 0 ||
newLocalDrivingLicenseApplicationsDTO.LicenseClassID < 0 
)


{

 return BadRequest("Invalid Data");
}

LocalDrivingLicenseApplicationsBusinessLayer.clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = new LocalDrivingLicenseApplicationsBusinessLayer.clsLocalDrivingLicenseApplications( new LocalDrivingLicenseApplicationsDTO(newLocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID,newLocalDrivingLicenseApplicationsDTO.ApplicationID,newLocalDrivingLicenseApplicationsDTO.LicenseClassID ) ) ;

LocalDrivingLicenseApplications.Save();
newLocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID = LocalDrivingLicenseApplications.LocalLicenseApplicationID;
return CreatedAtRoute("GetLocalDrivingLicenseApplicationsById", new { id = newLocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID }, newLocalDrivingLicenseApplicationsDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateLocalDrivingLicenseApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<LocalDrivingLicenseApplicationsDTO> UpdateLocalDrivingLicenseApplications(int id,LocalDrivingLicenseApplicationsDTO updatedLocalDrivingLicenseApplicationsDTO)
{

 if (id < 1 || updatedLocalDrivingLicenseApplicationsDTO == null || updatedLocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID < 0 ||
updatedLocalDrivingLicenseApplicationsDTO.ApplicationID < 0 ||
updatedLocalDrivingLicenseApplicationsDTO.LicenseClassID < 0 
)


{

 return BadRequest("Invalid Data");
}

 LocalDrivingLicenseApplicationsBusinessLayer.clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(id);

 if (LocalDrivingLicenseApplications == null)

{

return NotFound($"LocalDrivingLicenseApplications with ID {id} not found.");

}

LocalDrivingLicenseApplications.LocalLicenseApplicationID = updatedLocalDrivingLicenseApplicationsDTO.LocalLicenseApplicationID;
LocalDrivingLicenseApplications.ApplicationID = updatedLocalDrivingLicenseApplicationsDTO.ApplicationID;
LocalDrivingLicenseApplications.LicenseClassID = updatedLocalDrivingLicenseApplicationsDTO.LicenseClassID;

LocalDrivingLicenseApplications.Save();
 return Ok(LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteLocalDrivingLicenseApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteLocalDrivingLicenseApplications(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(LocalDrivingLicenseApplicationsBusinessLayer.clsLocalDrivingLicenseApplications.DeleteLocalDrivingLicenseApplications(id))

  return Ok($"LocalDrivingLicenseApplications with ID {id} has been deleted.");

else

return NotFound($"LocalDrivingLicenseApplications with ID {id} not found. no rows deleted!");

}
        [HttpGet("IsExistByID/{id}", Name = "IsLocalDrivingLicenseApplicationsExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsLocalDrivingLicenseApplicationsExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid License ID. Please provide a valid ID.");
            }

            bool exists = clsLocalDrivingLicenseApplications.isLocalDrivingLicenseApplicationsExist(id);


            if (exists)
            {
                return Ok($"Local Driving License Applications with ID {id} exists.");
            }
            else
            {
                return NotFound($"Local Driving License Applications with ID {id} does not exist.");
            }
        }



    }

}