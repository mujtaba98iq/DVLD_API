using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestsBusinessLayer;
using MyDVLDDataAccessLayer;
namespace TestsApi.Controllers
{

[ApiController]

[Route("api/Tests")]

public class TestsController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllTests")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<TestsDTO>> GetAllTests()
{

List<TestsDTO> TestsList = clsTests.GetAllTests();

 if (TestsList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(TestsList);

}


[HttpGet("GetByID/{id}", Name = "GetTestsById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<TestsDTO> GetTestsById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsTests Tests = clsTests.Find(id);

 if (Tests == null)

{

 return NotFound("Not Found");
}

TestsDTO TDTO = Tests.TestDTO;

 return Ok(TDTO);

}


[HttpPost("Add",Name = "AddTests")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<TestsDTO> AddTests(TestsDTO newTestsDTO)
{

 if (newTestsDTO == null || newTestsDTO.TestID < 1 ||
newTestsDTO.TestAppointmentID < 1 ||
string.IsNullOrEmpty(newTestsDTO.Notes) ||
newTestsDTO.CreatedByUserID < 1
)


{

 return BadRequest("Invalid Data");
}

TestsBusinessLayer.clsTests Tests = new TestsBusinessLayer.clsTests( new TestsDTO(newTestsDTO.TestID,newTestsDTO.TestAppointmentID,newTestsDTO.TestResult,newTestsDTO.Notes,newTestsDTO.CreatedByUserID ) ) ;

Tests.Save();
newTestsDTO.TestID = Tests.TestID;
return CreatedAtRoute("GetTestsById", new { id = newTestsDTO.TestID }, newTestsDTO);

}


[HttpPut("UpdateByID/{id}", Name = "UpdateTests")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<TestsDTO> UpdateTests(int id,TestsDTO updatedTestsDTO)
{

 if (id < 1 || updatedTestsDTO == null || updatedTestsDTO.TestID < 1 ||
updatedTestsDTO.TestAppointmentID < 1 ||
string.IsNullOrEmpty(updatedTestsDTO.Notes) ||
updatedTestsDTO.CreatedByUserID < 1
)


{

 return BadRequest("Invalid Data");
}

 TestsBusinessLayer.clsTests Tests = clsTests.Find(id);

 if (Tests == null)

{

return NotFound($"Tests with ID {id} not found.");

}

Tests.TestID = updatedTestsDTO.TestID;
Tests.TestAppointmentID = updatedTestsDTO.TestAppointmentID;
Tests.TestResult = updatedTestsDTO.TestResult;
Tests.Notes = updatedTestsDTO.Notes;
Tests.CreatedByUserID = updatedTestsDTO.CreatedByUserID;

Tests.Save();
 return Ok(Tests.TestDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteTests")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteTests(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(TestsBusinessLayer.clsTests.DeleteTests(id))

  return Ok($"Tests with ID {id} has been deleted.");

else

return NotFound($"Tests with ID {id} not found. no rows deleted!");

}

        [HttpGet("IsExistByID/{id}", Name = "IsTestExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult> IsTestExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Test ID. Please provide a valid ID.");
            }

            bool exists = await clsTests.isTestExist(id);

            if (exists)
            {
                return Ok($"Test with ID {id} exists.");
            }
            else
            {
                return NotFound($"Test with ID {id} does not exist.");
            }
        }



    }

}