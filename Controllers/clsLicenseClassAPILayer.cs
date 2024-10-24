using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LincnseClassesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace LincnseClassesApi.Controllers
{

[ApiController]

[Route("api/LincnseClasses")]

public class LincnseClassesController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllLincnseClasses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<LincnseClassesDTO>> GetAllLincnseClasses()
{

List<LincnseClassesDTO> LincnseClassesList = clsLincnseClasses.GetAllLincnseClasses();

 if (LincnseClassesList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(LincnseClassesList);

}


[HttpGet("GetByID/{id}", Name = "GetLincnseClassesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<LincnseClassesDTO> GetLincnseClassesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsLincnseClasses LincnseClasses = clsLincnseClasses.Find(id);

 if (LincnseClasses == null)

{

 return NotFound("Not Found");
}

LincnseClassesDTO LDTO = LincnseClasses.LincnseClassDTO;

 return Ok(LDTO);

}


[HttpPost("Add",Name = "AddLincnseClasses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<LincnseClassesDTO> AddLincnseClasses(LincnseClassesDTO newLincnseClassesDTO)
{

 if (newLincnseClassesDTO == null || newLincnseClassesDTO.LicenseClassID < 0 ||
string.IsNullOrEmpty(newLincnseClassesDTO.ClassName) ||
string.IsNullOrEmpty(newLincnseClassesDTO.ClassDescription) ||
newLincnseClassesDTO.MinimumAllowedAge < 0 ||
newLincnseClassesDTO.DefaultValidityLength < 0 ||
newLincnseClassesDTO.ClassFees < 0 
)


{

 return BadRequest("Invalid Data");
}

LincnseClassesBusinessLayer.clsLincnseClasses LincnseClasses = new LincnseClassesBusinessLayer.clsLincnseClasses( new LincnseClassesDTO(newLincnseClassesDTO.LicenseClassID,newLincnseClassesDTO.ClassName,newLincnseClassesDTO.ClassDescription,newLincnseClassesDTO.MinimumAllowedAge,newLincnseClassesDTO.DefaultValidityLength,newLincnseClassesDTO.ClassFees ) ) ;

LincnseClasses.Save();
newLincnseClassesDTO.LicenseClassID = LincnseClasses.LicenseClassID;
return CreatedAtRoute("GetLincnseClassesById", new { id = newLincnseClassesDTO.LicenseClassID }, newLincnseClassesDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateLincnseClasses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<LincnseClassesDTO> UpdateLincnseClasses(int id,LincnseClassesDTO updatedLincnseClassesDTO)
{

 if (id < 1 || updatedLincnseClassesDTO == null || updatedLincnseClassesDTO.LicenseClassID < 0 ||
string.IsNullOrEmpty(updatedLincnseClassesDTO.ClassName) ||
string.IsNullOrEmpty(updatedLincnseClassesDTO.ClassDescription) ||
updatedLincnseClassesDTO.MinimumAllowedAge < 0 ||
updatedLincnseClassesDTO.DefaultValidityLength < 0 ||
updatedLincnseClassesDTO.ClassFees < 0 
)


{

 return BadRequest("Invalid Data");
}

 LincnseClassesBusinessLayer.clsLincnseClasses LincnseClasses = clsLincnseClasses.Find(id);

 if (LincnseClasses == null)

{

return NotFound($"LincnseClasses with ID {id} not found.");

}

LincnseClasses.LicenseClassID = updatedLincnseClassesDTO.LicenseClassID;
LincnseClasses.ClassName = updatedLincnseClassesDTO.ClassName;
            LincnseClasses.ClassDescription = updatedLincnseClassesDTO.ClassDescription;
LincnseClasses.MinimumAllowedAge = updatedLincnseClassesDTO.MinimumAllowedAge;
LincnseClasses.DefaultValidityLength = updatedLincnseClassesDTO.DefaultValidityLength;
LincnseClasses.ClassFees = updatedLincnseClassesDTO.ClassFees;

LincnseClasses.Save();
 return Ok(LincnseClasses.LincnseClassDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteLincnseClasses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteLincnseClasses(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(LincnseClassesBusinessLayer.clsLincnseClasses.DeleteLincnseClasses(id))

  return Ok($"LincnseClasses with ID {id} has been deleted.");

else

return NotFound($"LincnseClasses with ID {id} not found. no rows deleted!");

}

        [HttpGet("IsExistByID/{id}", Name = "IsLicensesClassExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult IsLicensesClassExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid License ID. Please provide a valid ID.");
            }

            bool exists = clsLincnseClasses.isLicenseClassExist(id);


            if (exists)
            {
                return Ok($"License Class with ID {id} exists.");
            }
            else
            {
                return NotFound($"License Class with ID {id} does not exist.");
            }
        }



    }

}