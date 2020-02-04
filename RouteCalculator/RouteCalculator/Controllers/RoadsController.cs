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
    public class RoadsController : ControllerBase
    {
        private readonly IRoadsRepository _repository;
        public RoadsController(IRoadsRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllRoads());
            }
            catch (Exception)
            {

                return BadRequest("Oopss!");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Road road)
        {
            try
            {
                return Ok(_repository.AddRoad(road));
            }
            catch (Exception)
            {
                return BadRequest("Error while adding road");
            }
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Road road)
        {
            try
            {
                return Ok(_repository.EditRoad(road));
            }
            catch (Exception)
            {
                return BadRequest("Error occured while editing road");
            }
        }
    }
}