using SensorSimulator;
using SensorSimulator.Interfaces;
using SensorSimulator.Services;

var builder = Host.CreateApplicationBuilder(args);
// Реєстрація генератора даних як Singleton
builder.Services.AddSingleton<ISensorDataGenerator, BogusSensorDataGenerator>();

// Реєстрація HttpDataSender з використанням HttpClient
builder.Services.AddHttpClient<IDataSender, HttpDataSender>();

// Реєстрація фонового сервісу (Worker)
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

await host.RunAsync();
