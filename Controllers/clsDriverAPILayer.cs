using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DriversBusinessLayer;
using MyDVLDDataAccessLayer;
namespace DriversApi.Controllers
{

[ApiController]

[Route("api/Drivers")]

public class DriversController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllDrivers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<DriversDTO>> GetAllDrivers()
{

List<DriversDTO> DriversList = clsDrivers.GetAllDrivers();

 if (DriversList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(DriversList);

}


[HttpGet("GetByID/{id}", Name = "GetDriversById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<DriversDTO> GetDriversById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsDrivers Drivers = clsDrivers.Find(id);

 if (Drivers == null)

{

 return NotFound("Not Found");
}

DriversDTO DDTO = Drivers.DriverDTO;

 return Ok(DDTO);

}


[HttpPost("Add",Name = "AddDrivers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<DriversDTO> AddDrivers(DriversDTO newDriversDTO)
{

 if (newDriversDTO == null || newDriversDTO.DriverID < 1 ||
newDriversDTO.PersonID < 1||
newDriversDTO.CreatedByUserID < 1
)


{

 return BadRequest("Invalid Data");
}

DriversBusinessLayer.clsDrivers Drivers = new DriversBusinessLayer.clsDrivers( new DriversDTO(newDriversDTO.DriverID,newDriversDTO.PersonID,newDriversDTO.CreatedByUserID,newDriversDTO.CreatedDate ) ) ;

Drivers.Save();
newDriversDTO.DriverID = Drivers.DriverID;
return CreatedAtRoute("GetDriversById", new { id = newDriversDTO.DriverID }, newDriversDTO);

}
        [HttpPut("UpdateByID/{id}", Name = "UpdateDrivers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DriversDTO> UpdateDrivers(int id, DriversDTO updatedDriversDTO)
        {
            // التحقق من صحة البيانات المدخلة
            if (id < 1 || updatedDriversDTO == null || updatedDriversDTO.PersonID < 1 ||
                updatedDriversDTO.CreatedByUserID < 1)
            {
                return BadRequest("Invalid Data");
            }

            // البحث عن السائق حسب ID
            DriversBusinessLayer.clsDrivers Drivers = clsDrivers.Find(id);

            if (Drivers == null)
            {
                return NotFound($"Drivers with ID {id} not found.");
            }

            // التأكد من أن id يتطابق مع DriverID في DTO
            if (id != updatedDriversDTO.DriverID)
            {
                return BadRequest("Driver ID mismatch.");
            }

            // تحديث الحقول فقط (بدون تحديث DriverID)
            Drivers.PersonID = updatedDriversDTO.PersonID;
            Drivers.CreatedByUserID = updatedDriversDTO.CreatedByUserID;
            Drivers.CreatedDate = updatedDriversDTO.CreatedDate;

            // حفظ التحديثات
            Drivers.Save();

            return Ok(Drivers.DriverDTO);
        }



        [HttpDelete("DeleteByID/{id}", Name = "DeleteDrivers")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult DeleteDrivers(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

  if(DriversBusinessLayer.clsDrivers.DeleteDrivers(id))

  return Ok($"Drivers with ID {id} has been deleted.");

else

return NotFound($"Drivers with ID {id} not found. no rows deleted!");

}

        [HttpGet("IsExistByID/{id}", Name = "ExistDrivers")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult ExistDrivers(int id)
        {


          
            if (id < 1)

            {

                return BadRequest("Bad Request");
            }

            if (DriversBusinessLayer.clsDrivers.isDriverExist(id))

                return Ok($"Drivers with ID {id} is Exist.");

            else

                return NotFound($"Drivers with ID {id} not found. no is Exist!");

        }


    }

}