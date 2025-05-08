using SensorSimulator.Models;

namespace SensorSimulator.Interfaces;

public interface ISensorDataGenerator
{
    SensorData GenerateSensorData();
}