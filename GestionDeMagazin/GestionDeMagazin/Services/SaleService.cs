using GestionDeMagazin.Context;
using GestionDeMagazin.Models.Sales;
using GestionDeMagazin.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMagazin.Services;

public class SaleService : ISaleService
{
    private readonly AppDbContext _dbContext;

    public SaleService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Sale>> GetSales()
    {
        return await _dbContext.Sales.Include(s => s.SaleItems).ToListAsync();
    }

    public async Task<Sale> GetSale(int id)
    {
        return await _dbContext.Sales.Include(s => s.SaleItems).FirstOrDefaultAsync(s => s.SaleId == id);
    }

    public async Task<Sale> AddSale(Sale sale)
    {
        foreach (var saleItem in sale.SaleItems)
        {
            var product = await _dbContext.Products.FindAsync(saleItem.ProductId);

            if (product == null || product.Quantity < saleItem.Quantity)
            {
                throw new InvalidOperationException("Invalid product or insufficient quantity");
            }

            product.Quantity -= saleItem.Quantity;
        }
        await _dbContext.SaveChangesAsync();

        await _dbContext.Sales.FindAsync(sale);
        await _dbContext.SaveChangesAsync();

        return sale;
    }
}
