using Microsoft.AspNetCore.Mvc;
using ECommerce.DataService.PaymentData;
using ECommerce.DataService.PaymentModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ECommerce.DataService.PaymentModels.Payment;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
        private readonly DbContext_Payment _context;

        public PaymentServiceController(DbContext_Payment context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("CreatePayment")]
        public IActionResult CreatePayment(Payment payment)
        {
            var _payment = _context.Payments.Add(payment);
            return Ok(_payment);
        }

        [HttpGet("GetPaymentByID/{id}")]
        public IActionResult GetPaymentByID(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound(new { Message = "Payment not found." });
            }
            return Ok(payment);
        }

        [HttpPost("UpdateByID")]
        public IActionResult UpdateByID(Payment payment)
        {
            var _payment = _context.Payments.FirstOrDefault(p => p.PaymentId == payment.PaymentId);
            if (_payment == null)
            {
                return NotFound(new { Message = "Payment not found." });
            }
            //_payment.Amount = payment.Amount;
            //_payment.PaymentStatus = payment.PaymentStatus;
            //_payment.DateProcessed = payment.DateProcessed;
            _context.SaveChanges();
            return Ok(_payment);
        }

        [HttpGet("GetPayments")]
        public IActionResult GetPayments()
        {
            var payments = _context.Payments.ToList();
            return Ok(payments);
        }
    }
}
