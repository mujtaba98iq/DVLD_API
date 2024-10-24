using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CountriesBusinessLayer;
using MyDVLDDataAccessLayer;
namespace CountriesApi.Controllers
{

[ApiController]

[Route("api/Countries")]

public class CountriesController: ControllerBase
{
[HttpGet("GetAll", Name = "GetAllCountries")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
 public ActionResult<IEnumerable<CountriesDTO>> GetAllCountries()
{

List<CountriesDTO> CountriesList = clsCountries.GetAllCountries();

 if (CountriesList.Count == 0)

{

 return NotFound("Not Found");
}

return Ok(CountriesList);

}


[HttpGet("GetByID/{id}", Name = "GetCountriesById")] // Marks this method to respond to HTTP GET requests.
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
 public ActionResult<CountriesDTO> GetCountriesById(int id)
{

 if (id < 1)

{

 return BadRequest("Bad Request");
}

 clsCountries Countries = clsCountries.Find(id);

 if (Countries == null)

{

 return NotFound("Not Found");
}

CountriesDTO CDTO = Countries.CountryDTO;

 return Ok(CDTO);

}




}

}