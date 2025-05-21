using DataIngestionService.Interfaces;
using DataIngestionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataIngestionService.Controllers;

[ApiController]
[Route("sensor-data")]
public class IngestionController(ISensorDataSender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Ingest([FromBody] SensorData data)
    {
        var result = await sender.SendAsync(data);
        return result ? Ok("Надіслано") : StatusCode(500, "Помилка надсилання");
    }
}