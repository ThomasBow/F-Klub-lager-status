


using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        // GET: api/helloworld
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Default Warehouse Get Request (Not implemented)");
        }

        // GET: api/helloworld/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Hello, World! Your ID is {id}");
        }
    }
}