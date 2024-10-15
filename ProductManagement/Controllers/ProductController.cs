using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Domain;
using ProductManagement.Domain.Events;
using ProductManagement.Services;
using RabbitMQ.domain;

namespace ProductManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] Product product)
    {
        await _productService.RegisterProductAsync(product);
        return Ok();
    }

}
