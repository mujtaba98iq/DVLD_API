using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplcatoinTypesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace ApplcatoinTypesApi.Controllers
{

[ApiController]

[Route("api/ApplcatoinTypes")]

public class ApplcatoinTypesController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllApplcatoinTypes")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<ApplcatoinTypesDTO>> GetAllApplcatoinTypes()
{

List<ApplcatoinTypesDTO> ApplcatoinTypesList = clsApplcatoinTypes.GetAllApplcatoinTypes();

 if (ApplcatoinTypesList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(ApplcatoinTypesList);

}


[HttpGet("GetByID/{id}", Name = "GetApplcatoinTypesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<ApplcatoinTypesDTO> GetApplcatoinTypesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsApplcatoinTypes ApplcatoinTypes = clsApplcatoinTypes.Find(id);

 if (ApplcatoinTypes == null)

{

 return NotFound("Not Found");
}

ApplcatoinTypesDTO ADTO = ApplcatoinTypes.ApplcatoinTypeDTO;

 return Ok(ADTO);

}


[HttpPost("Add",Name = "AddApplcatoinTypes")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<ApplcatoinTypesDTO> AddApplcatoinTypes(ApplcatoinTypesDTO newApplcatoinTypesDTO)
{

 if (newApplcatoinTypesDTO == null || newApplcatoinTypesDTO.ApplicationTypeID < 0 ||
string.IsNullOrEmpty(newApplcatoinTypesDTO.ApplicationTypeTitle) ||
newApplcatoinTypesDTO.ApplicationFees < 0 
)


{

 return BadRequest("Invalid Data");
}

ApplcatoinTypesBusinessLayer.clsApplcatoinTypes ApplcatoinTypes = new ApplcatoinTypesBusinessLayer.clsApplcatoinTypes( new ApplcatoinTypesDTO(newApplcatoinTypesDTO.ApplicationTypeID,newApplcatoinTypesDTO.ApplicationTypeTitle,newApplcatoinTypesDTO.ApplicationFees ) ) ;

ApplcatoinTypes.Save();
newApplcatoinTypesDTO.ApplicationTypeID = ApplcatoinTypes.ApplicationTypeID;
return CreatedAtRoute("GetApplcatoinTypesById", new { id = newApplcatoinTypesDTO.ApplicationTypeID }, newApplcatoinTypesDTO);

}
        [HttpGet("IsExistByID/{id}", Name = "IsApplcatoinTypeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsApplcatoinTypeExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ApplcatoinType ID. Please provide a valid ID.");
            }

            bool exists = await clsApplcatoinTypes.isApplicationTypeExist(id);

            if (exists)
            {
                return Ok($"ApplcatoinType with ID {id} exists.");
            }
            else
            {
                return NotFound($"ApplcatoinType with ID {id} does not exist.");
            }
        }




    }

}