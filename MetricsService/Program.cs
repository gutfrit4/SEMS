using MetricsService.Monitoring;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Запускаємо метрики на /metrics
app.UseHttpMetrics();

// Ендпоінт, який змінює метрики (симуляція активності)
app.MapGet("/ping", () =>
{
    MetricsCollector.RequestCounter.Inc();
    MetricsCollector.RandomTemperature.Set(new Random().Next(20, 40));
});

app.MapGet("/error", () =>
{
    MetricsCollector.ErrorCounter.Inc();
    return Results.Problem("Simulated error");
});

app.UseRouting();

app.MapMetrics();  

app.Run("http://0.0.0.0:80");

