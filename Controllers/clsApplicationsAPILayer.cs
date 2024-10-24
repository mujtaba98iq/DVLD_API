using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationsBusinessLayer;
using MyDVLDDataAccessLayer;
namespace ApplicationsApi.Controllers
{

[ApiController]

[Route("api/Applications")]

public class ApplicationsController: ControllerBase
{
        private static readonly object _lock = new object();

        [HttpGet("GetAll", Name = "GetAllApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<ApplicationsDTO>> GetAllApplications()
{


                List<ApplicationsDTO> ApplicationsList = clsApplications.GetAllApplications();

                if (ApplicationsList.Count == 0)

                {

                    return NotFound("Not Found");
                }

                return Ok(ApplicationsList);

            }
        


[HttpGet("GetByID/{id}", Name = "GetApplicationsById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<ApplicationsDTO> GetApplicationsById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsApplications Applications = clsApplications.Find(id);

 if (Applications == null)

{

 return NotFound("Not Found");
}

ApplicationsDTO ADTO = Applications.ApplicationDTO;

 return Ok(ADTO);

}


[HttpPost("Add",Name = "AddApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<ApplicationsDTO> AddApplications(ApplicationsDTO newApplicationsDTO)
{

 if (newApplicationsDTO == null || newApplicationsDTO.ApplicationID < 1 ||
newApplicationsDTO.ApplicantPersonID < 1 ||
newApplicationsDTO.ApplicationTypeID < 1 ||
newApplicationsDTO.ApplicationStatus < 1 ||
newApplicationsDTO.PaidFees < 0 ||
newApplicationsDTO.CreatedByUserID < 1
)


            {

 return BadRequest("Invalid Data");
}

ApplicationsBusinessLayer.clsApplications Applications = new ApplicationsBusinessLayer.clsApplications( new ApplicationsDTO(newApplicationsDTO.ApplicationID,newApplicationsDTO.ApplicantPersonID,newApplicationsDTO.ApplicationDate,newApplicationsDTO.ApplicationTypeID,newApplicationsDTO.ApplicationStatus,newApplicationsDTO.LastStatusDate,newApplicationsDTO.PaidFees,newApplicationsDTO.CreatedByUserID ) ) ;

Applications.Save();
newApplicationsDTO.ApplicationID = Applications.ApplicationID;
return CreatedAtRoute("GetApplicationsById", new { id = newApplicationsDTO.ApplicationID }, newApplicationsDTO);

}


[HttpPut("UpdateByID/{id}",Name = "UpdateApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<ApplicationsDTO> UpdateApplications(int id,ApplicationsDTO updatedApplicationsDTO)
{

 if (id < 1 || updatedApplicationsDTO == null || updatedApplicationsDTO.ApplicationID < 1 ||
updatedApplicationsDTO.ApplicantPersonID < 1 ||
updatedApplicationsDTO.ApplicationTypeID < 1 ||
updatedApplicationsDTO.ApplicationStatus < 1 ||
updatedApplicationsDTO.PaidFees < 0 ||
updatedApplicationsDTO.CreatedByUserID < 1 
)


{

 return BadRequest("Invalid Data");
}

 ApplicationsBusinessLayer.clsApplications Applications = clsApplications.Find(id);

 if (Applications == null)

{

return NotFound($"Applications with ID {id} not found.");

}

Applications.ApplicationID = updatedApplicationsDTO.ApplicationID;
Applications.ApplicantPersonID = updatedApplicationsDTO.ApplicantPersonID;
Applications.ApplicationDate = updatedApplicationsDTO.ApplicationDate;
Applications.ApplicationTypeID = updatedApplicationsDTO.ApplicationTypeID;
Applications.ApplicationStatus = updatedApplicationsDTO.ApplicationStatus;
Applications.LastStatusDate = updatedApplicationsDTO.LastStatusDate;
Applications.PaidFees = updatedApplicationsDTO.PaidFees;
Applications.CreatedByUserID = updatedApplicationsDTO.CreatedByUserID;

Applications.Save();
 return Ok(Applications.ApplicationDTO);
}


[HttpDelete("DeleteByID/{id}",Name = "DeleteApplications")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteApplications(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(ApplicationsBusinessLayer.clsApplications.DeleteApplications(id))

  return Ok($"Applications with ID {id} has been deleted.");

else

return NotFound($"Applications with ID {id} not found. no rows deleted!");

}
        [HttpGet("IsExistByID/{id}", Name = "IsApplicationsExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsApplicationsExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Applications ID. Please provide a valid ID.");
            }

            bool exists = clsApplications.isApplicationsExist(id);

            if (exists)
            {
                return Ok($"Applications with ID {id} exists.");
            }
            else
            {
                return NotFound($"Applications with ID {id} does not exist.");
            }
        }


    }

}