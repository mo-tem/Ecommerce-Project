using Microsoft.AspNetCore.Mvc;
using EcomApi.Models;
using EcomApi.Data; // Needed for AppDbContext
using Microsoft.EntityFrameworkCore; // Needed for EF Core methods like ToListAsync

namespace EcomApi.Controllers
{
    // Marks this class as a Web API controller and enables automatic model binding
    [ApiController]

    // Sets the base route for this controller to /api/orders
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        // DbContext for accessing the database
        private readonly AppDbContext _context;

        // Inject AppDbContext via constructor
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        // Returns all orders from the database
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll() => _context.Orders.ToList();

        // POST: api/orders
        // Adds a new order to the database
        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            // Set the order date to the current UTC time
            order.OrderDate = DateTime.UtcNow;

            // Add the new order to the database
            _context.Orders.Add(order);

            // Save changes to persist the order
            _context.SaveChanges();

            // Return the newly created order
            return order;
        }

        // GET: api/orders/{id}
        // Retrieves a single order by its ID
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            // Find the order in the database
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            // Return 404 if not found, otherwise return the order
            return order == null ? NotFound() : order;
        }

        // PUT: api/orders/{id}
        // Updates an existing order in the database
        [HttpPut("{id}")]
        public IActionResult Update(int id, Order updated)
        {
            // Find the existing order
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            // Return 404 if not found
            if (order == null) return NotFound();

            // Update order properties
            order.ProductId = updated.ProductId;
            order.Quantity = updated.Quantity;

            // Save changes to persist updates
            _context.SaveChanges();

            // Return 204 No Content to indicate success
            return NoContent();
        }

        // DELETE: api/orders/{id}
        // Removes an order from the database
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Find the order to delete
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            // Return 404 if not found
            if (order == null) return NotFound();

            // Remove the order from the database
            _context.Orders.Remove(order);

            // Save changes to persist deletion
            _context.SaveChanges();

            // Return 204 No Content to indicate successful deletion
            return NoContent();
        }
    }
}
