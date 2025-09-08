using MongoDB.Driver; // Official MongoDB driver for .NET, used to connect and query MongoDB
using EcomApi.Models; // Access Product model

namespace EcomApi.Services
{
    // Service class for handling product-related database operations
    public class ProductsService
    {
        // Represents the "Products" collection in MongoDB
        private readonly IMongoCollection<Product> _products;

        // Constructor injects configuration (IConfiguration) to get connection string
        public ProductsService(IConfiguration config)
        {
            // Create MongoDB client using connection string from appsettings.json
            var client = new MongoClient(config.GetConnectionString("MongoDb"));

            // Get the database named "EcomDb"
            var database = client.GetDatabase("EcomDb");

            // Get the collection named "Products" from the database
            _products = database.GetCollection<Product>("Products");
        }

        // Retrieve all products from the MongoDB collection
        public List<Product> Get() => _products.Find(p => true).ToList();

        // Insert a new product into the MongoDB collection
        public Product Create(Product product)
        {
            _products.InsertOne(product); // Add product to database
            return product; // Return the newly created product
        }

        // TODO: Add Update/Delete methods similarly using _products.ReplaceOne or _products.DeleteOne
    }
}
