using System.Text.Json;
using SensorSimulator.Interfaces;
using SensorSimulator.Models;

namespace SensorSimulator;

public class Worker(
    ISensorDataGenerator sensorDataGenerator,
    IDataSender dataSender,
    ILogger<Worker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            SensorData data = sensorDataGenerator.GenerateSensorData();

            try
            {
                await dataSender.SendDataAsync(data, stoppingToken);
                logger.LogInformation("Дані успішно надіслані: {Data}", JsonSerializer.Serialize(data));
            }
            catch (Exception ex)
            {
                logger.LogError("Помилка при надсиланні даних: {Message}", ex.Message);
            }

            // Затримка між ітераціями, наприклад, 5 секунд
            await Task.Delay(5000, stoppingToken);
        }
    }
}