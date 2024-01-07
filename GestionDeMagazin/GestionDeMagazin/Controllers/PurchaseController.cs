using GestionDeMagazin.Models;
using GestionDeMagazin.Models.Purchases;
using GestionDeMagazin.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeMagazin.Controllers;
[Route("api/purchases")]
public class PurchaseController : Controller
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }
    // GET: api/purchases
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
    {
        var purchases = await _purchaseService.GetPurchases();
        return Ok(purchases);
    }

    // GET: api/purchases/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Purchase>> GetPurchase(int id)
    {
        var purchase =await _purchaseService.GetPurchase(id);

        if (purchase == null)
        {
            return NotFound();
        }

        return Ok(purchase);
    }

    // POST: api/purchases
    [HttpPost]
    public async Task<ActionResult<Purchase>> AddPurchase(Purchase purchase)
    {
        var addedPurchase =await _purchaseService.AddPurchase(purchase);
        return CreatedAtAction(nameof(GetPurchase), new { id = addedPurchase.PurchaseId }, addedPurchase);
    }

    // POST: api/purchases/{id}/receive
    [HttpPost("{id}/receive")]
    public async Task<IActionResult> ReceiveProducts(int id)
    {
        return await _purchaseService.ReceiveProducts(id);
    }

    // POST: api/purchases/{id}/return
    [HttpPost("{id}/return")]
    public async Task<IActionResult> ProcessReturn(int id, [FromBody] ReturnRequest returnRequest)
    {
        if (returnRequest == null || returnRequest.ProductId <= 0 || returnRequest.Quantity <= 0)
        {
            return BadRequest("Invalid return request");
        }

        return await _purchaseService.ProcessReturn(id, returnRequest.ProductId, returnRequest.Quantity);
    }
}
