namespace ProcessingService.Models;

public class SensorData
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

}