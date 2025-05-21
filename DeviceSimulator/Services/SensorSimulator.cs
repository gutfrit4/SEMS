using System.Text;
using System.Text.Json;
using DeviceSimulator.Models;

namespace DeviceSimulator.Services;

public class SensorSimulator(string endpoint)
{
    private readonly HttpClient _httpClient = new();
    private static readonly string[] DeviceIds = ["machine-1", "machine-2"];
    private readonly Random _rand = new();

    public async Task RunAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            foreach (var deviceId in DeviceIds)
            {
                var data = new SensorData
                {
                    DeviceId = deviceId,
                    Temperature = Math.Round(_rand.NextDouble() * 100, 1), // 0.0 – 100.0°C
                    Voltage = Math.Round(180 + _rand.NextDouble() * 60, 1) // 180.0 – 240.0V
                };

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await _httpClient.PostAsync(endpoint, content, token);
                    Console.WriteLine($"{DateTime.Now}: [{response.StatusCode}] {json}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }

            await Task.Delay(5000, token); // затримка між ітераціями
        }
    }
}