using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers 
{
    // api/users/4/meals
    [Route("api/users/{userId}/meals")]
    public class MaelsController : ControllerBase
    {
        // api/users/4/meals HTTP GET
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            return Ok();
        }

        // api/users/4/meals/16 HTTP GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int maelId)
        {
            return Ok();
        }

        // api/users/4/meals HTTP POST
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel InputModel)
        {
            return Ok();
        }

        // api/users/4/meals/16 HTTP PUT
        [HttpPut("{maelId}")]
        public IActionResult Put(int userId, int maelId, [FromBody] UpdateMealInputModel InputModel) 
        {
            return NoContent();
        }

        // api/users/4/meals/16 HTTP DELETE
        [HttpDelete("{maelId}")]
        public IActionResult Delete(int userId, int maelId)
        {
            return NoContent();
        }
    }
}
