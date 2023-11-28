using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecuture.Business.Services;
using NTierArchitecuture.DataAccess.Dtos;

namespace NTierArchitecuture.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductDto request, CancellationToken cancellationToken)
    {
        await _productService.AddAsync(request, cancellationToken);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        1
        return Ok(await _productService.GetAllAsync(cancellationToken));
    }
}