using CarAuctionManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : Controller
    {
        IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            this._auctionService = auctionService;
        }

        [HttpGet("star-auction/{vehicleId}")]
        public IActionResult StartAuctio(int vehicleId)
        {
            var result=_auctionService.StartAuction(vehicleId);
            if(result.Success==true)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpGet("close-auction/{vehicleId}")]
        public IActionResult CloseAuction(int vehicleId)
        {
            var result=_auctionService.CloseAuction(vehicleId);
            if(result.Success==true)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpGet("palce-bid/{vehicleId}/{bid}")]

        public IActionResult PlaceBid(int vehicleId, double bid)
        {
            var result = _auctionService.PlaceBid(vehicleId, bid);
            if (result.Success == true)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

    
    }
}
