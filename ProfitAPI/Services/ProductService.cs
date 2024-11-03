using ProfitAPI.Models;

namespace ProfitAPI.Services
{
    public class ProductService : IProductService
    {
        private List<LocalProduct> _products = new List<LocalProduct>
        {
            new LocalProduct { Id = 1, Name = "Product1", Description = "Description for Product1", Price = 10.5m, Stock = 100 },
            new LocalProduct { Id = 2, Name = "Product2", Description = "Description for Product2", Price = 20.0m, Stock = 50 }
        };

        public IEnumerable<LocalProduct> GetAllProducts() => _products;

        public LocalProduct? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(LocalProduct product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
        }

        public void UpdateProduct(LocalProduct product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct == null) return;
                
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description; 
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}