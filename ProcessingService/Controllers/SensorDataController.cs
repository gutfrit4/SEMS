using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcessingService.Data;
using ProcessingService.Interfaces;
using ProcessingService.Models;

namespace ProcessingService.Controllers;

[ApiController]
[Route("api/SensorData")]
public class SensorDataController(AppDbContext context, ISensorAnalyticsService analyticsService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SensorData data)
    {
        try
        {
            context.SensorData.Add(data);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Saved: {data.DeviceId} - {data.Temperature}°C - {data.Voltage}V");
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorData>>> Get()
    {
        try
        {
            var data = await context.SensorData
                .OrderByDescending(s => s.Timestamp)
                .Take(10)
                .ToListAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR: {ex.Message}");
            return StatusCode(500, ex.Message);
        }   
    }
    
    [HttpGet("analytics")]
    public async Task<ActionResult<IEnumerable<SensorDataAnalytics>>> GetAnalytics()
    {
        try
        {
            var result = await analyticsService.GetAnalyticsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR (Analytics): {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

}