namespace ProcessingService.Models;

public class SensorDataAnalytics
{
    public string DeviceId { get; set; } = string.Empty;
    public int Count { get; set; }
    public double AvgTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
}