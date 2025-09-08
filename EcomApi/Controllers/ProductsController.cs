using Microsoft.AspNetCore.Mvc;
using EcomApi.Models;

namespace EcomApi.Controllers
{
    // Marks this class as a Web API controller and enables automatic model binding
    [ApiController]

    // Sets the base route for this controller to /api/products
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // In-memory storage for products 
        private static List<Product> products = new();

        // GET: api/products
        // Returns all products in the in-memory list
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll() => products;

        // POST: api/products
        // Adds a new product to the list
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            // Generate a unique string ID for the product
            product.Id = Guid.NewGuid().ToString();

            // Add the product to the in-memory list
            products.Add(product);

            // Return the newly created product (201 Created)
            return product;
        }

        // GET: api/products/{id}
        // Retrieves a single product by its ID
        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            // Find the product with matching ID
            var product = products.FirstOrDefault(p => p.Id == id);

            // If not found, return 404 Not Found, else return the product
            return product == null ? NotFound() : product;
        }

        // PUT: api/products/{id}
        // Updates an existing product
        [HttpPut("{id}")]
        public IActionResult Update(string id, Product updated)
        {
            // Find the existing product
            var product = products.FirstOrDefault(p => p.Id == id);

            // Return 404 if not found
            if (product == null) return NotFound();

            // Update product properties
            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;

            // Return 204 No Content to indicate success
            return NoContent();
        }

        // DELETE: api/products/{id}
        // Removes a product from the list
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            // Find the product to delete
            var product = products.FirstOrDefault(p => p.Id == id);

            // Return 404 if not found
            if (product == null) return NotFound();

            // Remove the product from the list
            products.Remove(product);

            // Return 204 No Content to indicate successful deletion
            return NoContent();
        }
    }
}
