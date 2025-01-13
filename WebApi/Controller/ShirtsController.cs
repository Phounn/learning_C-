using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Filters.ActionFilter;
using WebApi.Filters.ExceptionFilter;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class ShirtsController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetAllShirts());
        }
        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id) // from"func" is to specific that the data must come from the rout. If it is not, then it will throw exception. be careful with using fromheader, fromquery. if use then no parameter 
        {
            //if (id <= 0) return BadRequest();
            //var shirt = ShirtRepository.GetShirtById(id);
            //if (shirt == null) return NotFound();
            //return Ok(shirt);
            return Ok(ShirtRepository.GetShirtById(id));


        }
        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirt([FromBody] Shirt shirt) //as same as fromform, but fromform must use postman in body, u must choose form-data
        {
            //if (shirt == null) return BadRequest();
            //var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
            //if (existingShirt != null) return BadRequest();
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }
        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }


    }
}
