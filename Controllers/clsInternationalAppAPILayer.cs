using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternationalLicensesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace InternationalLicensesApi.Controllers
{

[ApiController]

[Route("api/InternationalLicenses")]

public class InternationalLicensesController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllInternationalLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<InternationalLicensesDTO>> GetAllInternationalLicenses()
{

List<InternationalLicensesDTO> InternationalLicensesList = clsInternationalLicenses.GetAllInternationalLicenses();

 if (InternationalLicensesList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(InternationalLicensesList);

}


[HttpGet("GetByID/{id}", Name = "GetInternationalLicensesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<InternationalLicensesDTO> GetInternationalLicensesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsInternationalLicenses InternationalLicenses = clsInternationalLicenses.Find(id);

 if (InternationalLicenses == null)

{

 return NotFound("Not Found");
}

InternationalLicensesDTO InternationalLicensesDTO = InternationalLicenses.InternationalLicensDTO;

 return Ok(InternationalLicensesDTO);

}

        [HttpPost("Add",Name = "AddInternationalLicenses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<InternationalLicensesDTO> AddInternationalLicenses(InternationalLicensesDTO newInternationalLicensesDTO)
        {
            // فحص صحة المدخلات
            if (newInternationalLicensesDTO == null || newInternationalLicensesDTO.InternationalAppID < 1 ||
                newInternationalLicensesDTO.ApplicatoinID < 1 ||
                newInternationalLicensesDTO.DirverID < 1 ||
                newInternationalLicensesDTO.LocalDriverApplicationID < 1 ||
                newInternationalLicensesDTO.CreatedByUserID < 1)
            {
                return BadRequest("Invalid Data: Please check ApplicationID, DriverID, and other required fields.");
            }

            // إنشاء الكائن
            InternationalLicensesBusinessLayer.clsInternationalLicenses InternationalLicenses =
                new InternationalLicensesBusinessLayer.clsInternationalLicenses(
                    new InternationalLicensesDTO(
                        newInternationalLicensesDTO.InternationalAppID,
                        newInternationalLicensesDTO.ApplicatoinID,
                        newInternationalLicensesDTO.DirverID,
                        newInternationalLicensesDTO.LocalDriverApplicationID,
                        newInternationalLicensesDTO.IssueDate,
                        newInternationalLicensesDTO.ExpirationDate,
                        newInternationalLicensesDTO.IsActive,
                        newInternationalLicensesDTO.CreatedByUserID
                    ));

            // استدعاء دالة الحفظ
            InternationalLicenses.Save();

            // إرجاع الاستجابة بعد الحفظ
            newInternationalLicensesDTO.InternationalAppID = InternationalLicenses.InternationalAppID;
            return CreatedAtRoute("GetInternationalLicensesById", new { id = newInternationalLicensesDTO.InternationalAppID }, newInternationalLicensesDTO);
        }



        [HttpPut("UpdateByID/{id}", Name = "UpdateInternationalLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<InternationalLicensesDTO> UpdateInternationalLicenses(int id,InternationalLicensesDTO updatedInternationalLicensesDTO)
{

 if (id < 1 || updatedInternationalLicensesDTO == null || updatedInternationalLicensesDTO.InternationalAppID < 1 ||
updatedInternationalLicensesDTO.ApplicatoinID < 1||
updatedInternationalLicensesDTO.DirverID < 1 ||
updatedInternationalLicensesDTO.LocalDriverApplicationID < 1 ||
updatedInternationalLicensesDTO.CreatedByUserID < 1 
)


{

 return BadRequest("Invalid Data");
}

 InternationalLicensesBusinessLayer.clsInternationalLicenses InternationalLicenses = clsInternationalLicenses.Find(id);

 if (InternationalLicenses == null)

{

return NotFound($"InternationalLicenses with ID {id} not found.");

}

InternationalLicenses.InternationalAppID = updatedInternationalLicensesDTO.InternationalAppID;
InternationalLicenses.ApplicatoinID = updatedInternationalLicensesDTO.ApplicatoinID;
InternationalLicenses.DirverID = updatedInternationalLicensesDTO.DirverID;
InternationalLicenses.LocalDriverApplicationID = updatedInternationalLicensesDTO.LocalDriverApplicationID;
InternationalLicenses.IssueDate = updatedInternationalLicensesDTO.IssueDate;
InternationalLicenses.ExpirationDate = updatedInternationalLicensesDTO.ExpirationDate;
InternationalLicenses.IsActive = updatedInternationalLicensesDTO.IsActive;
InternationalLicenses.CreatedByUserID = updatedInternationalLicensesDTO.CreatedByUserID;

InternationalLicenses.Save();
 return Ok(InternationalLicenses.InternationalLicensDTO);
}


[HttpDelete("DeleteByID/{id}", Name = "DeleteInternationalLicenses")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteInternationalLicenses(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(InternationalLicensesBusinessLayer.clsInternationalLicenses.DeleteInternationalLicenses(id))

  return Ok($"InternationalLicenses with ID {id} has been deleted.");

else

return NotFound($"InternationalLicenses with ID {id} not found. no rows deleted!");

}



}

}