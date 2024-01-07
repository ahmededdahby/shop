using GestionDeMagazin.Models;
using GestionDeMagazin.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeMagazin.Controllers;

[Route("api/products")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var pro = await _productService.GetProducts();
        return Ok(pro);
    }
    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProduct(id);

        if (product.Id == 0)
        {
            return NotFound();
        }

        return Ok(product);
    }
    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        var res = await _productService.AddProduct(product);
        return StatusCode(201);
    }
    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var res = await _productService.DeleteProduct(id);

        if (res == "Not Found")
        {
            return NotFound();
        }
        return Ok(res);
    }
}
