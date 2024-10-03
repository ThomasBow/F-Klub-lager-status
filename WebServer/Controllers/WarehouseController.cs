


using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using WebServer.Requests;
using WebServer.Exceptions;
using BlazorFrontend.Shared.Models;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        readonly Warehouse warehouse;

        public WarehouseController(Warehouse warehouse) 
        { 
            this.warehouse = warehouse;
        }

        [HttpPost("dev/testData")]
        public IActionResult PopulateWithTestData()
        {
            warehouse.AddNewItem("1981", "Sports Cola");

            string guid = Guid.NewGuid().ToString();
            warehouse.AddNewItem(guid, "Cocio");

            warehouse.AddNewItem("wow", "Popcorn");


            warehouse.AddAmountToItem("1981", 200);

            int amount = new Random().Next();
            warehouse.AddAmountToItem(guid, amount);

            warehouse.SubtractAmountFromItem("wow", 143);

            return Ok("Populated server with test data.");
        }

        [HttpGet]
        public IActionResult GetStatus() 
        {
            try
            {
                List<Item> items = new();
                foreach (WarehouseItem whItem in warehouse.Items)
                {
                    items.Add( new(whItem.Identifier, whItem.Name, whItem.Amount) );
                }

                WarehouseStatusResponse response = new()
                {
                    Items = items,
                    LastChanged = warehouse.lastChange
                };
                
                return Ok(JsonSerializer.Serialize(response));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("item/add")]
        public IActionResult AddNewItem([FromBody] ItemRequest request)
        {
            try
            {
                warehouse.AddNewItem(request.Identifier, request.Name);
                return Ok($"Item {request.Name} with identifier {request.Identifier} added.");
            }
            catch (AlreadyExistsException)
            {
                return Conflict("Item with given identifier already exists!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("item/{identifier}/add-stock")]
        public IActionResult AddStock(string identifier, [FromBody] ModifyStockRequest request)
        {
            try
            { 
                if (request.Amount <= 0) return BadRequest("Amount should be greater than 0!");

                warehouse.AddAmountToItem(identifier, request.Amount);
                return Ok($"Added {request.Amount} to item with identifier {identifier}");
            }
            catch (NotFoundException)
            {
                return NotFound($"Item with identifier {identifier} not found!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("item/{identifier}/remove-stock")]
        public IActionResult RemoveStock(string identifier, [FromBody] ModifyStockRequest request)
        {
            try
            {
                if (request.Amount <= 0) return BadRequest("Amount should be greater than 0!");

                warehouse.SubtractAmountFromItem(identifier, request.Amount);
                return Ok($"Removed {request.Amount} from item with identifier {identifier}");
            }
            catch (NotFoundException)
            {
                return NotFound($"Item with identifier {identifier} not found!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("item/{identifier}")]
        public IActionResult GetItem(string identifier)
        {
            try
            {
                WarehouseItem item = warehouse.FindItem(identifier);
                return Ok(JsonSerializer.Serialize(item));
            }
            catch (NotFoundException)
            {
                return NotFound($"Item with identifier {identifier} not found!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("item/{identifier}")]
        public IActionResult DeleteItem(string identifier)
        {
            try
            {
                warehouse.DeleteItem(identifier);
                return Ok($"Item with identifier {identifier} deleted.");
            }
            catch (NotFoundException)
            {
                return NotFound($"Item with identifier {identifier} not found!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}