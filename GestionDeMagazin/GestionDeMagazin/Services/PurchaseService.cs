using GestionDeMagazin.Context;
using GestionDeMagazin.Models.Purchases;
using GestionDeMagazin.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMagazin.Services;

public class PurchaseService : IPurchaseService
{
    private readonly AppDbContext _dbContext; 

    public PurchaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Purchase>> GetPurchases()
    {
        return await _dbContext.Purchases.Include(p => p.PurchaseItems).ToListAsync();
    }

    public async Task<Purchase> GetPurchase(int id)
    {
        return await _dbContext.Purchases.Include(p => p.PurchaseItems).FirstOrDefaultAsync(p => p.PurchaseId == id);
    }

    public async Task<Purchase> AddPurchase(Purchase purchase)
    {
        await _dbContext.Purchases.AddAsync(purchase);
        await _dbContext.SaveChangesAsync();

        return purchase;
    }

    public async Task<IActionResult> ReceiveProducts(int purchaseId)
    {
        var purchase = await _dbContext.Purchases.Include(p => p.PurchaseItems).FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

        if (purchase == null)
        {
            return new NotFoundResult();
        }

        foreach (var purchaseItem in purchase.PurchaseItems)
        {
            var product = await _dbContext.Products.FindAsync(purchaseItem.ProductId);

            if (product == null)
            {
                return new BadRequestResult();
            }

            // Update product quantity in stock
            product.Quantity += purchaseItem.Quantity;
        }

        await _dbContext.SaveChangesAsync();

        return new OkResult();
    }

    public async Task<IActionResult> ProcessReturn(int purchaseId, int productId, int quantity)
    {
        var purchase = await _dbContext.Purchases.Include(p => p.PurchaseItems).FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

        if (purchase == null)
        {
            return new NotFoundResult();
        }

        var purchaseItem = purchase.PurchaseItems.FirstOrDefault(item => item.ProductId == productId);

        if (purchaseItem == null || purchaseItem.Quantity < quantity)
        {
            return new BadRequestResult();
        }

        // Process return and update product quantity in stock
        var product = await _dbContext.Products.FindAsync(productId);
        product.Quantity -= quantity;

        await _dbContext.SaveChangesAsync();

        return new OkResult();
    }
}
