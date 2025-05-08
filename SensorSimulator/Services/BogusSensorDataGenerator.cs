using Bogus;
using SensorSimulator.Interfaces;
using SensorSimulator.Models;

namespace SensorSimulator.Services;

public class BogusSensorDataGenerator : ISensorDataGenerator
{
    private readonly Faker<SensorData> _faker;

    public BogusSensorDataGenerator()
    {
        _faker = new Faker<SensorData>()
            .RuleFor(s => s.Temperature, f => f.Random.Double(20, 100))
            .RuleFor(s => s.Pressure, f => f.Random.Double(1, 10))
            .RuleFor(s => s.Timestamp, f => DateTime.UtcNow);
    }
    
    public SensorData GenerateSensorData()
    {
        return _faker.Generate();
    }
}