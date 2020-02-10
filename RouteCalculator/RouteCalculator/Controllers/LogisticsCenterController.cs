using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteCalculator.Data.Interfaces;

namespace RouteCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogisticsCenterController : ControllerBase
    {

        private readonly ILogisticsCentersRepository _repository;
        public LogisticsCenterController(ILogisticsCentersRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/LogisticsCenter
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetLogisticsCenter());
            }
            catch (Exception e)
            {
                return BadRequest("An error accured while getting logistics center");
            }
        }

      
        [HttpPost]
        public IActionResult Create()
        {
            try
            {             
                return Ok(_repository.CalculateLogisticsCenter());
            }
            catch (Exception )
            {
                return BadRequest("An error accured while creating logistics center");
            }        
        }
  
    }
}
