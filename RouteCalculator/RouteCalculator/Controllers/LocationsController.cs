using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteCalculator.Data.Interfaces;
using Router.Domain;

namespace RouteCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsRepository _repository;
        public LocationsController(ILocationsRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllLocations());
            }
            catch (Exception )
            {
                return BadRequest("Oopss!");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Location location)
        {
            try
            {
                return Ok(_repository.AddLocation(location));
            }
            catch (Exception)
            {
                return BadRequest("Error while adding location");
            }
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Location location)
        {
            try
            {
                return Ok(_repository.EditLocation(location));
            }
            catch (Exception)
            {
                return BadRequest("Error occured while editing location");
            }
        }
    }
}
