using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
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
        public string CreateShirt([FromBody] Shirt shirt) //as same as fromform, but fromform must use postman in body, u must choose form-data
        {
            return "Creating A Shirt";
        }
        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating The Shirt With ID: {id}";
        }
        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Deleting The Shirt With ID: {id}";
        }


    }
}
