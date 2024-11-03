using Microsoft.AspNetCore.Mvc;
using ProfitAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confiti.MoySklad.Remap.Entities;

[ApiController]
[Route("api/[controller]")]
public class MoySkladController : ControllerBase
{
    private readonly MoySkladService _moySkladService;

    public MoySkladController(MoySkladService moySkladService)
    {
        _moySkladService = moySkladService;
    }
 
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _moySkladService.GetProductsAsync();
        return Ok(products);
    }
}