using Microsoft.AspNetCore.Mvc;
using ECommerce.DataService.ProductData;
using ECommerce.DataService.ProductModels.Product;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductServiceController : ControllerBase
    {
        private readonly DbContext_Product _context;

        public ProductServiceController(DbContext_Product context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            var _product = _context.Products.Add(product);
            return Ok(_product);
        }

        [HttpGet("GetProductByID/{id}")]
        public IActionResult GetProductByID(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }
            return Ok(product);
        }

        [HttpPost("UpdateByID")]
        public IActionResult UpdateByID(Product product)
        {
            var _product = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (_product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }
            //_product.Name = product.Name;
            //_product.Description = product.Description;
            //_product.Price = product.Price;
            //_product.Quantity = product.Quantity;
            //_product.ModifiedBy = product.ModifiedBy;
            //_product.ModifiedDate = product.ModifiedDate;
            _context.SaveChanges();
            return Ok(_product);
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
    }
}
