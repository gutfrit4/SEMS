using System.Text;
using System.Text.Json;
using DataIngestionService.Interfaces;
using DataIngestionService.Models;

namespace DataIngestionService.Services;

public class SensorDataSender(HttpClient httpClient, ILogger<SensorDataSender> logger) : ISensorDataSender
{
    public async Task<bool> SendAsync(SensorData data)
    {
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync("http://processing:80/api/SensorData", content); // 5002 — порт ProcessingService

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Помилка при надсиланні даних");
            return false;
        }
    }
}