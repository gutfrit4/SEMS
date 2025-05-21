using ApiGateway.Interfaces;
using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("sensor-data")]
public class SensorController(ISensorService sensorService, IHistoryService historyService, ILogger<SensorController> logger)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SensorData data)
    {
        var response = await sensorService.ForwardSensorDataAsync(data);
        var content = await response.Content.ReadAsStringAsync();
        if (data.DeviceId == "machine-1")
        {
            CustomMetrics.LastMachine1Temperature.Set(data.Temperature);
            CustomMetrics.LastMachine1Voltage.Set(data.Voltage);
        }
        else if (data.DeviceId == "machine-2")
        {
            CustomMetrics.LastMachine2Temperature.Set(data.Temperature);
            CustomMetrics.LastMachine2Voltage.Set(data.Voltage);
        }
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("⚠️ Error forwarding to DataIngestion: {Content}", content);
            return StatusCode((int)response.StatusCode, $"Upstream error: {content}");
        }
        return Ok(content);
    }
    
    [HttpGet("sensor-history")]
    public async Task<IActionResult> Get()
    {
        try
        {
            CustomMetrics.HistoryRequests.Inc();
            var history = await historyService.GetHistoryAsync();
            return Ok(history);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Gateway error: {ex.Message}");
        }
    }
}