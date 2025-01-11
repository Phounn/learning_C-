using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public string GetShirts()
        {
            return "Reading All The Shirts";
        }
        [HttpGet("{id}")]
        public string GetShirtById(int id)
        {
            return $"Reading The Shirt With ID: {id}";
        }
        [HttpPost]
        public string CreateShirt()
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
