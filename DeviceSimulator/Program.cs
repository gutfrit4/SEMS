using DeviceSimulator.Services;

Console.WriteLine("▶️ Sensor Simulator started. Press Ctrl+C to exit...");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("\n Завершення роботи...");
    cts.Cancel();
    e.Cancel = true;
};

const string ingestionEndpoint = "http://apigateway:80/sensor-data";
var simulator = new SensorSimulator(ingestionEndpoint);
await simulator.RunAsync(cts.Token);