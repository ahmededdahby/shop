using GestionDeMagazin.Models.Sales;
using GestionDeMagazin.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeMagazin.Controllers;
[Route("api/sales")]
public class SaleController : Controller
{
    private readonly ISaleService _salesService;

    public SaleController(ISaleService salesService)
    {
        _salesService = salesService;
    }
    // GET: api/sales
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
    {
        var sales = await _salesService.GetSales();
        return Ok(sales);
    }

    // GET: api/sales/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetSale(int id)
    {
        var sale = await _salesService.GetSale(id);

        if (sale == null)
        {
            return NotFound();
        }

        return Ok(sale);
    }

    // POST: api/sales
    [HttpPost]
    public async Task<ActionResult<Sale>> AddSale(Sale sale)
    {
        try
        {
            var addedSale =await _salesService.AddSale(sale);
            return CreatedAtAction(nameof(GetSale), new { id = addedSale.SaleId }, addedSale);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
