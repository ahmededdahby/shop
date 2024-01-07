using GestionDeMagazin.Context;
using GestionDeMagazin.Models;
using GestionDeMagazin.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMagazin.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _db.Products.ToListAsync();
    }
    public async Task<Product> AddProduct(Product product)
    {
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<string> DeleteProduct(int id)
    {
        var product = await _db.Products.FindAsync(id);

        if (product == null)
        {
            return "Not Found";
        }

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        return "Deleted successfully";
    }

    public async Task<Product> GetProduct(int id)
    {
        var product = await _db.Products.FindAsync(id);

        if (product == null)
        {
            return new Product() { Id= 0};
        }

        return product;
    }

    
}
