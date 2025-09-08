using Microsoft.AspNetCore.Mvc;
using EcomApi.Models;

namespace EcomApi.Controllers
{
    // Marks this class as a Web API controller and enables automatic model binding
    [ApiController]

    // Sets the base route for this controller to /api/orders
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        // In-memory storage for orders
        private static List<Order> orders = new();

        // GET: api/orders
        // Returns all orders in the in-memory list
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll() => orders;

        // POST: api/orders
        // Adds a new order to the list
        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            // Auto-generate a unique integer ID for the order
            order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;

            // Set the order date to the current UTC time
            order.OrderDate = DateTime.UtcNow;

            // Add the new order to the in-memory list
            orders.Add(order);

            // Return the newly created order
            return order;
        }

        // GET: api/orders/{id}
        // Retrieves a single order by its ID
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            // Find the order with matching ID
            var order = orders.FirstOrDefault(o => o.Id == id);

            // Return 404 if not found, otherwise return the order
            return order == null ? NotFound() : order;
        }

        // PUT: api/orders/{id}
        // Updates an existing order
        [HttpPut("{id}")]
        public IActionResult Update(int id, Order updated)
        {
            // Find the existing order
            var order = orders.FirstOrDefault(o => o.Id == id);

            // Return 404 if not found
            if (order == null) return NotFound();

            // Update order properties
            order.ProductId = updated.ProductId;
            order.Quantity = updated.Quantity;

            // Return 204 No Content to indicate success
            return NoContent();
        }

        // DELETE: api/orders/{id}
        // Removes an order from the list
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Find the order to delete
            var order = orders.FirstOrDefault(o => o.Id == id);

            // Return 404 if not found
            if (order == null) return NotFound();

            // Remove the order from the in-memory list
            orders.Remove(order);

            // Return 204 No Content to indicate successful deletion
            return NoContent();
        }
    }
}