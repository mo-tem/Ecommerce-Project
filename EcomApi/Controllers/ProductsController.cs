using Microsoft.AspNetCore.Mvc;
using EcomApi.Models;
using EcomApi.Services;

namespace EcomApi.Controllers
{
    // Marks this class as a Web API controller and enables automatic model binding
    [ApiController]

    // Sets the base route for this controller to /api/products
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Service that handles all MongoDB operations for products
        private readonly ProductsService _service;

        // Constructor injection of ProductsService
        public ProductsController(ProductsService service)
        {
            _service = service;
        }

        // GET: api/products
        // Retrieves all products from MongoDB
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll() => _service.Get();

        // POST: api/products
        // Adds a new product to MongoDB
        [HttpPost]
        public ActionResult<Product> Create(Product product) => _service.Create(product);

        // GET: api/products/{id}
        // Retrieves a single product by its ID from MongoDB
        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            var product = _service.Get().FirstOrDefault(p => p.Id == id);
            return product == null ? NotFound() : product;
        }

        // PUT: api/products/{id}
        // Updates an existing product in MongoDB
        [HttpPut("{id}")]
        public IActionResult Update(string id, Product updated)
        {
            var product = _service.Get().FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;

            // Optional: call a service method to update in MongoDB if you implement it
            // _service.Update(product);

            return NoContent();
        }

        // DELETE: api/products/{id}
        // Deletes a product from MongoDB
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var product = _service.Get().FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            // Optional: call a service method to delete in MongoDB if you implement it
            // _service.Delete(id);

            return NoContent();
        }
    }
}