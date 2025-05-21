using System.Text.Json;
using ApiGateway.Interfaces;
using ApiGateway.Models;

namespace ApiGateway.Services;

public class HistoryService(HttpClient httpClient) : IHistoryService
{
    private const string TargetUrl = "http://processing:80/api/SensorData";
    
    public async Task<IEnumerable<SensorData>> GetHistoryAsync()
    {
        var response = await httpClient.GetAsync(TargetUrl);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<SensorData>>(json) ?? new List<SensorData>();
    }
}