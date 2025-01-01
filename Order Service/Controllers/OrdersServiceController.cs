using Microsoft.AspNetCore.Mvc;
using ECommerce.DataService.OrderData;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ECommerce.DataService.OrderModels.Order;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersServiceController : ControllerBase
    {
        private readonly DbContext_Order _context;

        public OrdersServiceController(DbContext_Order context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(Order order)
        {
            var _order = _context.Orders.Add(order);
            return Ok(_order);
        }

        [HttpGet("GetOrderByID/{id}")]
        public IActionResult GetOrderByID(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound(new { Message = "Order not found." });
            }
            return Ok(order);
        }

        [HttpPost("UpdateByID")]
        public IActionResult UpdateByID(Order order)
        {
            var _order = _context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (_order == null)
            {
                return NotFound(new { Message = "Order not found." });
            }
            //_order.Status = order.Status;
            //_order.ModifiedBy = order.ModifiedBy;
            //_order.ModifiedDate = order.ModifiedDate;
            _context.SaveChanges();
            return Ok(_order);
        }

        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }
    }
}
