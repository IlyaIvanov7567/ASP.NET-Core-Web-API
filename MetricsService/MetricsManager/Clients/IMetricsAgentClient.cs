using MetricsAgent.Jobs;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        MetricsResponse<CpuMetric> GetCpuMetrics(MetricGetRequest<CpuMetric> request);
        MetricsResponse<HddMetric> GetHddMetrics(MetricGetRequest<HddMetric> request);
        MetricsResponse<DotNetMetric> GetDotNetMetrics(MetricGetRequest<DotNetMetric> request);
        MetricsResponse<RamMetric> GetRamMetrics(MetricGetRequest<RamMetric> request);
        MetricsResponse<NetworkMetric> GetNetworkMetrics(MetricGetRequest<NetworkMetric> request);
    }
}