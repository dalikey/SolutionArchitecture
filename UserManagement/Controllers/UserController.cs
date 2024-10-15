using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain;
using UserManagement.Services;

namespace UserManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly CsvImportService _csvImportService;

    public UserController(UserService userService, CsvImportService csvImportService)
    {
        _userService = userService;
        _csvImportService = csvImportService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] User user)
    {
        await _userService.RegisterUserAsync(user);
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        await _userService.UpdateUserAsync(user);
        return Ok();
    }

    [HttpPost]
    [Route("ticket/{userId}")]
    public async Task<IActionResult> CreateSupportTicket([FromBody] Support support, [FromRoute] int userId)
    {
        await _userService.RequestUserSupport(support, userId);
        return Ok();
    }

    [HttpPost]
    [Route("import-csv")]
    public async Task<IActionResult> ImportUsersFromCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File not selected");

        var filePath = Path.GetTempFileName();

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await _csvImportService.ImportUsersFromCsvAsync(filePath);

        return Ok("Users imported successfully");
    }
}
