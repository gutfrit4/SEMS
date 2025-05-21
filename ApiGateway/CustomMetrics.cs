using Prometheus;

namespace ApiGateway;

public class CustomMetrics
{
    public static readonly Counter ForwardedRequests = Metrics
        .CreateCounter("forwarded_requests_total", "Кількість успішних запитів на пересилання");

    public static readonly Counter ForwardErrors = Metrics
        .CreateCounter("forward_errors_total", "Кількість помилок при пересиланні");

    public static readonly Counter HistoryRequests = Metrics
        .CreateCounter("history_requests_total", "Кількість звернень до історії даних");

    public static readonly Gauge LastMachine1Temperature = Metrics
        .CreateGauge("machine1_last_temperature", "Остання температура зі станка 1");

    public static readonly Gauge LastMachine2Temperature = Metrics
        .CreateGauge("machine2_last_temperature", "Остання температура зі станка 2");

    public static readonly Gauge LastMachine1Voltage = Metrics
        .CreateGauge("machine1_last_voltage", "Остання напруга зі станка 1");

    public static readonly Gauge LastMachine2Voltage = Metrics
        .CreateGauge("machine2_last_voltage", "Остання напруга зі станка 2");
}