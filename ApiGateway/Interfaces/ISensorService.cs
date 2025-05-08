using ApiGateway.Models;

namespace ApiGateway.Interfaces;

public interface ISensorService
{
    Task<HttpResponseMessage> ForwardSensorDataAsync(SensorData data);
}