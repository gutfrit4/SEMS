using Microsoft.AspNetCore.Mvc;
using ProcessingService.Data;
using ProcessingService.Interfaces;
using ProcessingService.Models;

namespace ProcessingService.Controllers;

[ApiController]
[Route("api/SensorData")]
public class SensorDataController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SensorData data)
    {
        try
        {
            context.SensorData.Add(data);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Saved: {data.DeviceId} - {data.Temperature}");
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }
}