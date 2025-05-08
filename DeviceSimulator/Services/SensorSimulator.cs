using System.Text;
using System.Text.Json;
using DeviceSimulator.Models;

namespace DeviceSimulator.Services;

public class SensorSimulator
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;

    public SensorSimulator(string endpoint)
    {
        _httpClient = new HttpClient();
        _endpoint = endpoint;
    }

    public async Task RunAsync(CancellationToken token)
    {
        var rand = new Random();
        while (!token.IsCancellationRequested)
        {
            var data = new SensorData
            {
                DeviceId = "sensor-" + rand.Next(1, 100).ToString("D3"),
                Temperature = Math.Round(rand.NextDouble() * 40, 1) // 0.0 – 40.0
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(_endpoint, content, token);
                Console.WriteLine($"{DateTime.Now}: [{response.StatusCode}] {json}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            await Task.Delay(5000, token); // 5 сек інтервал
        }
    }
}