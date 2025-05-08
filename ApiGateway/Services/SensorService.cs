using System.Text;
using System.Text.Json;
using ApiGateway.Interfaces;
using ApiGateway.Models;

namespace ApiGateway.Services;

public class SensorService(HttpClient httpClient, IConfiguration config) : ISensorService
{
    public async Task<HttpResponseMessage> ForwardSensorDataAsync(SensorData data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var ingestionUrl = config["IngestionServiceUrl"] ?? throw new InvalidOperationException("IngestionServiceUrl not configured");
        return await httpClient.PostAsync($"{ingestionUrl}/api/ingestion", content);
    }
}