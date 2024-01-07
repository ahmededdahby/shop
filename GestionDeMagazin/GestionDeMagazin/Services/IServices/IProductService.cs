using GestionDeMagazin.Models;

namespace GestionDeMagazin.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task<Product> AddProduct(Product product);
    Task<string> DeleteProduct(int id);
}
