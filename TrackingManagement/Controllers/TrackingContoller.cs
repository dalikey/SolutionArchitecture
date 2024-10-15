using TrackingManagement.Domain;
using TrackingManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace TrackingManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackingController : ControllerBase
{
    private readonly TrackingService _trackingService;

    public TrackingController(TrackingService trackingService)
    {
        _trackingService = trackingService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterTracking([FromBody] TrackingData tracking)
    {


        await _trackingService.RegisterTrackingAsync(tracking);
        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateTracking([FromBody] TrackingData tracking)
    {


        await _trackingService.UpdateTrackingAsync(tracking);
        return Ok();
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetTrackingAsync([FromBody] int trackingId)
    {
        await _trackingService.GetTrackingAsync(trackingId);
        return Ok();
    }
}
