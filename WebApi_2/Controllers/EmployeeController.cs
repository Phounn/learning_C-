using Microsoft.AspNetCore.Mvc;
using WebApi_2.Repositories;

namespace WebApi_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllEmps()
        {
            return Ok( EmployeeRepository.GetAllEmployee());
        }
    }
}
