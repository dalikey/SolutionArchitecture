using Microsoft.AspNetCore.Mvc;
using OrderManagement.Services;
using OrderManagement.Domain;

namespace OrderManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("addorder")]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        Console.WriteLine("Added order");
        
        var result = await _orderService.AddOrder(order);
        return result ? Ok() : BadRequest();
    }

    [HttpPut]
    [Route("updateorder")]
    public async Task<IActionResult> UpdateOrder([FromBody] Order order)
    {
        Console.WriteLine("Updated order");
        
        var result = await _orderService.UpdateOrder(order);
        return result ? Ok() : BadRequest();
    }

    [HttpPut]
    [Route("cancelorder/{OrderNumber}")]
    public async Task<IActionResult> CancelOrder(string OrderNumber)
    {
        Console.WriteLine("Cancelled order: " + OrderNumber);
        
        var result = await _orderService.CancelOrder(OrderNumber);
        return result ? Ok() : BadRequest();
    }

    
}
