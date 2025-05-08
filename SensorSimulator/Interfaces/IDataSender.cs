using SensorSimulator.Models;

namespace SensorSimulator.Interfaces;

public interface IDataSender
{
    Task SendDataAsync(SensorData data, CancellationToken cancellationToken);
}