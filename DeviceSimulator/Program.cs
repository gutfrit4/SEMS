using DeviceSimulator.Services;

Console.WriteLine("▶️ Sensor Simulator started. Press Ctrl+C to exit...");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("\n Завершення роботи...");
    cts.Cancel();
    e.Cancel = true;
};

string ingestionEndpoint = "http://datain:80/api/ingestion"; // DataIngestionService
var simulator = new SensorSimulator(ingestionEndpoint);
await simulator.RunAsync(cts.Token);