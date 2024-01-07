using GestionDeMagazin.Models.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeMagazin.Services.IServices;

public interface IPurchaseService
{
    Task<IEnumerable<Purchase>> GetPurchases();
    Task<Purchase> GetPurchase(int id);
    Task<Purchase> AddPurchase(Purchase purchase);
    Task<IActionResult> ReceiveProducts(int purchaseId);
    Task<IActionResult> ProcessReturn(int purchaseId, int productId, int quantity);
}
