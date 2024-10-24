using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTypesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace TestTypesApi.Controllers
{

[ApiController]

[Route("api/TestTypes")]

public class TestTypesController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllTestTypes")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<TestTypesDTO>> GetAllTestTypes()
{

List<TestTypesDTO> TestTypesList = clsTestTypes.GetAllTestTypes();

 if (TestTypesList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(TestTypesList);

}


[HttpGet("GetByID/{id}", Name = "GetTestTypesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<TestTypesDTO> GetTestTypesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsTestTypes TestTypes = clsTestTypes.Find(id);

 if (TestTypes == null)

{

 return NotFound("Not Found");
}

TestTypesDTO TDTO = TestTypes.TestTypeDTO;

 return Ok(TDTO);

}


[HttpPost("Add",Name = "AddTestTypes")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<TestTypesDTO> AddTestTypes(TestTypesDTO newTestTypesDTO)
{

 if (newTestTypesDTO == null || newTestTypesDTO.TestTypeID < 0 ||
string.IsNullOrEmpty(newTestTypesDTO.TestTypeTitle) ||
string.IsNullOrEmpty(newTestTypesDTO.TestTypeDescription) ||
newTestTypesDTO.TestFees < 0 
)


{

 return BadRequest("Invalid Data");
}

TestTypesBusinessLayer.clsTestTypes TestTypes = new TestTypesBusinessLayer.clsTestTypes( new TestTypesDTO(newTestTypesDTO.TestTypeID,newTestTypesDTO.TestTypeTitle,newTestTypesDTO.TestTypeDescription,newTestTypesDTO.TestFees ) ) ;

TestTypes.Save();
newTestTypesDTO.TestTypeID = TestTypes.TestTypeID;
return CreatedAtRoute("GetTestTypesById", new { id = newTestTypesDTO.TestTypeID }, newTestTypesDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateTestTypes")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<TestTypesDTO> UpdateTestTypes(int id,TestTypesDTO updatedTestTypesDTO)
{

 if (id < 1 || updatedTestTypesDTO == null || updatedTestTypesDTO.TestTypeID < 0 ||
string.IsNullOrEmpty(updatedTestTypesDTO.TestTypeTitle) ||
string.IsNullOrEmpty(updatedTestTypesDTO.TestTypeDescription) ||
updatedTestTypesDTO.TestFees < 0 
)


{

 return BadRequest("Invalid Data");
}

 TestTypesBusinessLayer.clsTestTypes TestTypes = clsTestTypes.Find(id);

 if (TestTypes == null)

{

return NotFound($"TestTypes with ID {id} not found.");

}

TestTypes.TestTypeID = updatedTestTypesDTO.TestTypeID;
TestTypes.TestTypeTitle = updatedTestTypesDTO.TestTypeTitle;
TestTypes.TestTypeDescription = updatedTestTypesDTO.TestTypeDescription;
TestTypes.TestFees = updatedTestTypesDTO.TestFees;

TestTypes.Save();
 return Ok(TestTypes.TestTypeDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteTestTypes")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteTestTypes(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(TestTypesBusinessLayer.clsTestTypes.DeleteTestTypes(id))

  return Ok($"TestTypes with ID {id} has been deleted.");

else

return NotFound($"TestTypes with ID {id} not found. no rows deleted!");

}
        [HttpGet("IsExistByID/{id}", Name = "IsTestTypeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult> IsTestTypeExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid TestType ID. Please provide a valid ID.");
            }

            bool exists = await clsTestTypes.isTestTypeExist(id);

            if (exists)
            {
                return Ok($"TestType with ID {id} exists.");
            }
            else
            {
                return NotFound($"TestType with ID {id} does not exist.");
            }
        }



    }

}