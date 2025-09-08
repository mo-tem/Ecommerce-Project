namespace EcomApi.Models
{
    // Represents an order placed by a customer
    public class Order
    {
        // Unique identifier for the order (primary key)
        public int Id { get; set; }

        // ID of the product associated with this order
        // Initialized to empty string to avoid nulls
        public string ProductId { get; set; } = string.Empty;

        // Quantity of the product in this order
        public int Quantity { get; set; }

        // Date and time when the order was created
        // Defaults to the current UTC time
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}