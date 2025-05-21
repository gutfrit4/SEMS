namespace ApiGateway.Models;

public class SensorData
{
    public string DeviceId { get; set; } = string.Empty;
    
    public double Temperature { get; set; }
    public double Voltage { get; set; }
}