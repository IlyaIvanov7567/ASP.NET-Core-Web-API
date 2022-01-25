using Core.DAL.Models;
using Core.DAL.Requests;
using Core.DAL.Responses;

namespace MetricsManager.Clients
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