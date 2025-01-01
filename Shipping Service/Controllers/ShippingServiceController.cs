using Microsoft.AspNetCore.Mvc;
using ECommerce.DataService.ShippingData;
using Microsoft.AspNetCore.Authorization;
using ECommerce.DataService.ShippingModels.Shipping;

namespace ShippingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingServiceController : ControllerBase
    {
        private readonly DbContext_Shipping _context;

        public ShippingServiceController(DbContext_Shipping context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("CreateShipping")]
        public IActionResult CreateShipping(Shipping shipping)
        {
            var _shipping = _context.Shippings.Add(shipping);
            return Ok(_shipping);
        }

        [HttpGet("GetShippingByID/{id}")]
        public IActionResult GetShippingByID(int id)
        {
            var shipping = _context.Shippings.FirstOrDefault(s => s.ShippingId == id);
            if (shipping == null)
            {
                return NotFound(new { Message = "Shipping not found." });
            }
            return Ok(shipping);
        }

        [HttpPost("UpdateByID")]
        public IActionResult UpdateByID(Shipping shipping)
        {
            var _shipping = _context.Shippings.FirstOrDefault(s => s.ShippingId == shipping.ShippingId);
            if (_shipping == null)
            {
                return NotFound(new { Message = "Shipping not found." });
            }
            //_shipping.Address = shipping.Address;
            //_shipping.DateShipped = shipping.DateShipped;
            _shipping.ShippingStatus = shipping.ShippingStatus;
            _context.SaveChanges();
            return Ok(_shipping);
        }

        [HttpGet("GetShippings")]
        public IActionResult GetShippings()
        {
            var shippings = _context.Shippings.ToList();
            return Ok(shippings);
        }
    }
}
