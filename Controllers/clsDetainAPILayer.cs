using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DetainedLicensesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace DetainedLicensesApi.Controllers
{

[ApiController]

[Route("api/DetainedLicenses")]

public class DetainedLicensesController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllDetainedLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<DetainedLicensesDTO>> GetAllDetainedLicenses()
{

List<DetainedLicensesDTO> DetainedLicensesList = clsDetainedLicenses.GetAllDetainedLicenses();

 if (DetainedLicensesList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(DetainedLicensesList);

}


[HttpGet("GetByID/{id}", Name = "GetDetainedLicensesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<DetainedLicensesDTO> GetDetainedLicensesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsDetainedLicenses DetainedLicenses = clsDetainedLicenses.Find(id);

 if (DetainedLicenses == null)

{

 return NotFound("Not Found");
}

DetainedLicensesDTO DDTO = DetainedLicenses.DetainedLicensDTO;

 return Ok(DDTO);

}


[HttpPost("Add",Name = "AddDetainedLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<DetainedLicensesDTO> AddDetainedLicenses(DetainedLicensesDTO newDetainedLicensesDTO)
{

 if (newDetainedLicensesDTO == null || newDetainedLicensesDTO.DetainID < 1 ||
newDetainedLicensesDTO.LicenseID < 1 ||
newDetainedLicensesDTO.FineFees < 0 ||
newDetainedLicensesDTO.CreatedByUserID < 1 ||
newDetainedLicensesDTO.ReleasedByUserID < 1 ||
newDetainedLicensesDTO.ReleaseApplicationID < 1
)


{

 return BadRequest("Invalid Data");
}

DetainedLicensesBusinessLayer.clsDetainedLicenses DetainedLicenses = new DetainedLicensesBusinessLayer.clsDetainedLicenses( new DetainedLicensesDTO(newDetainedLicensesDTO.DetainID,newDetainedLicensesDTO.LicenseID,newDetainedLicensesDTO.DetainDate,newDetainedLicensesDTO.FineFees,newDetainedLicensesDTO.CreatedByUserID,newDetainedLicensesDTO.IsReleased,newDetainedLicensesDTO.ReleaseDate,newDetainedLicensesDTO.ReleasedByUserID,newDetainedLicensesDTO.ReleaseApplicationID ) ) ;

DetainedLicenses.Save();
newDetainedLicensesDTO.DetainID = DetainedLicenses.DetainID;
return CreatedAtRoute("GetDetainedLicensesById", new { Id = newDetainedLicensesDTO.DetainID }, newDetainedLicensesDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateDetainedLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<DetainedLicensesDTO> UpdateDetainedLicenses(int id,DetainedLicensesDTO updatedDetainedLicensesDTO)
{

 if (id < 1 || updatedDetainedLicensesDTO == null || updatedDetainedLicensesDTO.DetainID < 1 ||
updatedDetainedLicensesDTO.LicenseID < 1 ||
updatedDetainedLicensesDTO.FineFees < 0 ||
updatedDetainedLicensesDTO.CreatedByUserID < 1 ||
updatedDetainedLicensesDTO.ReleasedByUserID < 1 ||
updatedDetainedLicensesDTO.ReleaseApplicationID < 1 
)


{

 return BadRequest("Invalid Data");
}

 DetainedLicensesBusinessLayer.clsDetainedLicenses DetainedLicenses = clsDetainedLicenses.Find(id);

 if (DetainedLicenses == null)

{

return NotFound($"DetainedLicenses with ID {id} not found.");

}

DetainedLicenses.DetainID = updatedDetainedLicensesDTO.DetainID;
DetainedLicenses.LicenseID = updatedDetainedLicensesDTO.LicenseID;
DetainedLicenses.DetainDate = updatedDetainedLicensesDTO.DetainDate;
DetainedLicenses.FineFees = updatedDetainedLicensesDTO.FineFees;
DetainedLicenses.CreatedByUserID = updatedDetainedLicensesDTO.CreatedByUserID;
DetainedLicenses.IsReleased = updatedDetainedLicensesDTO.IsReleased;
DetainedLicenses.ReleaseDate = updatedDetainedLicensesDTO.ReleaseDate;
DetainedLicenses.ReleasedByUserID = updatedDetainedLicensesDTO.ReleasedByUserID;
DetainedLicenses.ReleaseApplicationID = updatedDetainedLicensesDTO.ReleaseApplicationID;

DetainedLicenses.Save();
 return Ok(DetainedLicenses.DetainedLicensDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteDetainedLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteDetainedLicenses(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(DetainedLicensesBusinessLayer.clsDetainedLicenses.DeleteDetainedLicenses(id))

  return Ok($"DetainedLicenses with ID {id} has been deleted.");

else

return NotFound($"DetainedLicenses with ID {id} not found. no rows deleted!");

}
        [HttpGet("IsExistByID/{id}", Name = "IsDetainedLicensesExist")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult> IsDetainedLicensesExist(int id)
        {



            if (id < 1)

            {

                return BadRequest("Bad Request");
            }

            if (await clsDetainedLicenses.isDetainExist(id))

                return Ok($"DetainedLicenses with ID {id} is Exist.");

            else

                return NotFound($"DetainedLicenses with ID {id} not found. no is Exist!");

        }




    }

}