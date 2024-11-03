using ProfitAPI.Models;

namespace ProfitAPI.Services;

public interface IProductService
{
    IEnumerable<LocalProduct> GetAllProducts();
    LocalProduct? GetProductById(int id); 
    void AddProduct(LocalProduct product);
    void UpdateProduct(LocalProduct product);
    void DeleteProduct(int id);
}