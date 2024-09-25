using CarAuctionManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarAuctionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [HttpPost]
        public IActionResult Add([FromBody]Dictionary<string,object>constructorParams)
        {
            var result = _vehicleService.AddVehicle(constructorParams);
            if (result.Success == true)
                return Ok(result.Message);
            return BadRequest(result.Message);

        }

        [HttpGet("byType/{type}")]
        public IActionResult GetByType(string type)
        {
            var result = _vehicleService.GetByType(type);

            if (result.Success == true)
                return Ok(JsonSerializer.Serialize(result.Data.Select(v => v.ToString())));

            return StatusCode(500, result.Message);
        }

        [HttpGet("byManufacturer/{manufacturer}")]
        public IActionResult GetByManufacturer(string manufacturer)
        {
            var result = _vehicleService.GetByManufacturer(manufacturer);

            if (result.Success == true)
                return Ok(JsonSerializer.Serialize(result.Data.Select(v => v.ToString())));

            return StatusCode(500, result.Message);

        }

        [HttpGet("byModel/{model}")]
        public IActionResult GetByModel(string model)
        {
            var result = _vehicleService.GetByModel(model);
            if (result.Success == true)
                return Ok(JsonSerializer.Serialize(result.Data.Select(v => v.ToString())));
            return StatusCode(500, result.Message);

        }

        [HttpGet("byYear/{year}")]
        public IActionResult GetByyear(int year)
        {
            var result = _vehicleService.GetByYear(year);
            if (result.Success == true)
                return Ok(JsonSerializer.Serialize(result.Data.Select(v => v.ToString())));
            return StatusCode(500, result.Message);

        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _vehicleService.GetAll();
            if (result.Success == true)
                return Ok(JsonSerializer.Serialize(result.Data.Select(v => v.ToString())));
            return StatusCode(500, result.Message);
        }
    }
}
