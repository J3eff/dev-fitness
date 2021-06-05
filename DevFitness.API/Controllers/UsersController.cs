using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    //api/users
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // api/users/1 método HTTP GET
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // return NotFound();

            return Ok();
        }

        // api/users método HTTP POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel) 
        {
            //retutn BadRequest();

            return CreatedAtAction(nameof(Get), new { id = 10 }, inputModel);
        }

        // api/users/id HTTP PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] UpdateUserInputModel inputModel)
        {
            // return NotFound();
            // return BadRequest();

            return NoContent();            
        }
    }
}
