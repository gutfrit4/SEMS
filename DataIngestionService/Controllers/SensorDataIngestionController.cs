using Microsoft.AspNetCore.Mvc;
using SensorSimulator.Models;

namespace DataIngestionService.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorDataIngestionController(ILogger<SensorDataIngestionController> logger) : ControllerBase
{
    // Симуляція зберігання даних (для демонстрації)
    private static readonly List<SensorData> SensorDataStore = new List<SensorData>();

    [HttpPost]
    public IActionResult Post([FromBody] SensorData sensorData)
    {
        if (sensorData == null)
        {
            return BadRequest("Дані не надано.");
        }

        // Логування отриманих даних
        logger.LogInformation("Отримано дані: Temperature: {Temperature}, Pressure: {Pressure}, Timestamp: {Timestamp}",
            sensorData.Temperature, sensorData.Pressure, sensorData.Timestamp);

        // Збереження даних (пізніше можна змінити на збереження в БД)
        SensorDataStore.Add(sensorData);

        return Ok(new { message = "Дані успішно збережено", data = sensorData });
    }

    // Додатковий GET endpoint для перевірки збережених даних
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(SensorDataStore);
    }
}