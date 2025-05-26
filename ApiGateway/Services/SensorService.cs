using System.Text;
using System.Text.Json;
using ApiGateway.Interfaces;
using ApiGateway.Models;

namespace ApiGateway.Services;

public class SensorService(HttpClient? httpClient) : ISensorService
{
    
    private readonly ILogger<SensorService> _logger;

    private const string TargetUrl = "http://datain:80/sensor-data"; // Заміни на актуальну адресу


    public async Task<HttpResponseMessage> ForwardSensorDataAsync(SensorData data)
    {
        Console.WriteLine("[SEND] " + JsonSerializer.Serialize(data));
        
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );
        // var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(TargetUrl, content);

            if (response.IsSuccessStatusCode)
            {
                CustomMetrics.ForwardedRequests.Inc(); // ✅ рахуємо успішні запити
            }
            else
            {
                CustomMetrics.ForwardErrors.Inc(); // ❌ рахуємо помилки
                _logger.LogWarning("Forward failed: {StatusCode}", response.StatusCode);
            }

            return response;
        }
        catch (Exception ex)
        {
            CustomMetrics.ForwardErrors.Inc(); // ❌ у разі виключення — теж фіксуємо помилку
            _logger.LogError(ex, "Exception while forwarding sensor data");
            throw;
        }
    }
}