using Microsoft.AspNetCore.Mvc;
using OrderManagement.Services;
using OrderManagement.Domain;

namespace OrderManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Route("addproduct")]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        Console.WriteLine("Added product");

        var result = await _productService.AddProduct(product);
        return result ? Ok() : BadRequest();
    }

    [HttpPut]
    [Route("updateproduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        Console.WriteLine("Updated product");

        var result = await _productService.UpdateProduct(product);
        return result ? Ok() : BadRequest();
    }
}
