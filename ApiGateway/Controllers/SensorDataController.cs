using System.Text;
using System.Text.Json;
using SensorSimulator.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorDataController(ILogger<SensorDataController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SensorData sensorData)
    {
        if (sensorData == null)
        {
            return BadRequest("Отримано порожні дані");
        }

        logger.LogInformation("Отримано дані: Temperature: {Temperature}, Pressure: {Pressure}, Timestamp: {Timestamp}",
            sensorData.Temperature, sensorData.Pressure, sensorData.Timestamp);

        // Пересилання даних до Data Ingestion Service
        using (var httpClient = new HttpClient())
        {
            //URL на фактичну адресу Data Ingestion Service
            var json = JsonSerializer.Serialize(sensorData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("http://localhost:5129/SensorDataIngestion", content);
        }

        return Ok(new { message = "Дані отримано та переслано" });
    }
    
}