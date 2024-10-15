using Microsoft.AspNetCore.Mvc;
using OrderManagement.Services;
using OrderManagement.Domain;

namespace OrderManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("adduser")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        Console.WriteLine("Added user");

        var result = await _userService.AddUser(user);
        return result ? Ok() : BadRequest();
    }

    [HttpPut]
    [Route("updateuser")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        Console.WriteLine("Updated user");

        var result = await _userService.UpdateUser(user);
        return result ? Ok() : BadRequest();
    }
}
