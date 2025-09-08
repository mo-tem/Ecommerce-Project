namespace EcomApi.Models
{
    // Represents a product in the e-commerce system
    public class Product
    {
        // Unique identifier for the product (primary key)
        // Initialized to empty string to avoid nulls
        public string Id { get; set; } = string.Empty;

        // Name of the product
        // Initialized to empty string to avoid nulls
        public string Name { get; set; } = string.Empty;

        // Price of the product
        public decimal Price { get; set; }

        // Number of units available in stock
        public int Stock { get; set; }
    }
}
