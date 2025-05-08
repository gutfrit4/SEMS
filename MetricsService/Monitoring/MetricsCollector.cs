using Prometheus;

namespace MetricsService.Monitoring;

public class MetricsCollector
{
    public static readonly Counter RequestCounter = Metrics
        .CreateCounter("metrics_service_requests_total", "Total number of metric requests received.");

    public static readonly Counter ErrorCounter = Metrics
        .CreateCounter("metrics_service_errors_total", "Total number of errors occurred.");

    public static readonly Gauge RandomTemperature = Metrics
        .CreateGauge("metrics_service_fake_temperature", "Random fake temperature for testing.");
    
}