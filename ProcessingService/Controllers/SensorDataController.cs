using Microsoft.AspNetCore.Mvc;
using ProcessingService.Interfaces;
using ProcessingService.Models;

namespace ProcessingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorDataController(ISensorDataService sensorDataService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostSensorData([FromBody] SensorData data)
    {
        var result = await sensorDataService.CreateAsync(data);
        return Ok(new { status = "saved", result.Id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await sensorDataService.GetAllAsync();
        return Ok(data);
    }
}