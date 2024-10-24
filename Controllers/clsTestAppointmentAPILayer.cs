using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppointmentBusinessLayer;
using MyDVLDDataAccessLayer;
namespace TestAppointmentApi.Controllers
{

[ApiController]

[Route("api/TestAppointment")]

public class TestAppointmentController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllTestAppointment")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<TestAppointmentDTO>> GetAllTestAppointment()
{

List<TestAppointmentDTO> TestAppointmentList = clsTestAppointment.GetAllTestAppointment();

 if (TestAppointmentList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(TestAppointmentList);

}


[HttpGet("GetByID/{id}", Name = "GetTestAppointmentById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<TestAppointmentDTO> GetTestAppointmentById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsTestAppointment TestAppointment = clsTestAppointment.Find(id);

 if (TestAppointment == null)

{

 return NotFound("Not Found");
}

TestAppointmentDTO TDTO = TestAppointment.TestAppointmentDTO;

 return Ok(TDTO);

}

        [HttpPost("Add",Name = "AddTestAppointment")] // Marks this method to respond to HTTP POST requests.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestAppointmentDTO> AddTestAppointment(TestAppointmentDTO newTestAppointmentDTO)
        {
            // تحقق من صحة البيانات المدخلة
            if (newTestAppointmentDTO == null ||
                newTestAppointmentDTO.TestAppointmentID < 1 ||
                newTestAppointmentDTO.TestTypeID < 1 ||
                newTestAppointmentDTO.LocalDrivingLicenseApplicationID < 0 ||
                newTestAppointmentDTO.PaidFees < 0 ||
                newTestAppointmentDTO.CreatedByUserID < 1)
            {
                return BadRequest("Invalid Data");
            }

            // قم بإنشاء كائن clsTestAppointment ومرر البيانات
            var testAppointment = new TestAppointmentBusinessLayer.clsTestAppointment(newTestAppointmentDTO);

            // حفظ الحجز
            var result = testAppointment.Save();

            if (result) // تأكد من أن الحفظ تم بنجاح
            {
                newTestAppointmentDTO.TestAppointmentID =testAppointment.TestAppointmentID; // تحديث معرف الحجز الجديد
                return CreatedAtRoute("GetTestAppointmentById", new { id = newTestAppointmentDTO.TestAppointmentID }, newTestAppointmentDTO);
            }

            return StatusCode(500, "An error occurred while creating the appointment.");
        }



        [HttpPut("UpdateByID/{id}", Name = "UpdateTestAppointment")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<TestAppointmentDTO> UpdateTestAppointment(int id,TestAppointmentDTO updatedTestAppointmentDTO)
{

 if (id < 1 || updatedTestAppointmentDTO == null || updatedTestAppointmentDTO.TestAppointmentID < 1 ||
updatedTestAppointmentDTO.TestTypeID < 1 ||
updatedTestAppointmentDTO.LocalDrivingLicenseApplicationID <1 ||
updatedTestAppointmentDTO.PaidFees < 0 ||
updatedTestAppointmentDTO.CreatedByUserID < 1 ||
updatedTestAppointmentDTO.RetakeTestApplicationID < 1 
)


{

 return BadRequest("Invalid Data");
}

 TestAppointmentBusinessLayer.clsTestAppointment TestAppointment = clsTestAppointment.Find(id);

 if (TestAppointment == null)

{

return NotFound($"TestAppointment with ID {id} not found.");

}

TestAppointment.TestAppointmentID = updatedTestAppointmentDTO.TestAppointmentID;
TestAppointment.TestTypeID = updatedTestAppointmentDTO.TestTypeID;
TestAppointment.LocalDrivingLicenseApplicationID = updatedTestAppointmentDTO.LocalDrivingLicenseApplicationID;
TestAppointment.AppointmentDate = updatedTestAppointmentDTO.AppointmentDate;
TestAppointment.PaidFees = updatedTestAppointmentDTO.PaidFees;
TestAppointment.CreatedByUserID = updatedTestAppointmentDTO.CreatedByUserID;
            TestAppointment.IsLocked = updatedTestAppointmentDTO.IsLocked;
TestAppointment.RetakeTestApplicationID = updatedTestAppointmentDTO.RetakeTestApplicationID;

TestAppointment.Save();
 return Ok(TestAppointment.TestAppointmentDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteTestAppointment")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteTestAppointment(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(TestAppointmentBusinessLayer.clsTestAppointment.DeleteTestAppointment(id))

  return Ok($"TestAppointment with ID {id} has been deleted.");

else

return NotFound($"TestAppointment with ID {id} not found. no rows deleted!");

}

        [HttpGet("IsExistByID/{id}", Name = "IsTestAppointmentExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult> IsTestAppointmentExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Test ID. Please provide a valid ID.");
            }

            bool exists = await clsTestAppointment.isTestAppointmentExist(id);

            if (exists)
            {
                return Ok($"TestAppointment with ID {id} exists.");
            }
            else
            {
                return NotFound($"TestAppointment with ID {id} does not exist.");
            }
        }


    }

}