using System.Text;
using System.Text.Json;
using SensorSimulator.Interfaces;
using SensorSimulator.Models;

namespace SensorSimulator.Services;

public class HttpDataSender(HttpClient httpClient) : IDataSender
{
    public async Task SendDataAsync(SensorData data, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Замініть URL на реальний endpoint API Gateway
        var response = await httpClient.PostAsync("http://localhost:5152/SensorData", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Помилка при надсиланні даних: {response.StatusCode}");
        }
    }
}