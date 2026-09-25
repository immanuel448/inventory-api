using InventoryApi.DTOs;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

//Activa comportamientos propios de las APIs, incluyendo la validación automática de los DTOs.
[ApiController]
//Define la ruta base:
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    //endpoints
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }
}