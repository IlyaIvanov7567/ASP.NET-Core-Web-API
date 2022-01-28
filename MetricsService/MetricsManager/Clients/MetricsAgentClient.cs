using System.Net.Http;
using System.Text.Json;
using Core.DAL.Models;
using Core.DAL.Requests;
using Core.DAL.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Clients
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(
            HttpClient httpClient, 
            IHttpClientFactory clientFactory, 
            IConfiguration configuration, 
            ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("AgentBaseUrl");
            _logger = logger;
        }

        public MetricsResponse<CpuMetric> GetCpuMetrics(MetricGetRequest<CpuMetric> request)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{_baseUrl}/api/metrics/cpu/getbyinterval/from/{request.FromTime}/to/{request.ToTime}");

            requestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");

            var responseMessage = _httpClient.SendAsync(requestMessage).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <MetricsResponse<CpuMetric>>(responseStream).Result;
                return metricsResponse;
            }
            else
            {
                _logger.LogError($"Response error: {responseMessage.ReasonPhrase}");
            }

            return null;
        }

        public MetricsResponse<HddMetric> GetHddMetrics(MetricGetRequest<HddMetric> request)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{_baseUrl}/api/metrics/hdd/getbyinterval/from/{request.FromTime}/to/{request.ToTime}");

            requestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");
            
            var responseMessage = _httpClient.SendAsync(requestMessage).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <MetricsResponse<HddMetric>>(responseStream).Result;
                return metricsResponse;
            }
            else
            {
                _logger.LogError($"Response error: {responseMessage.ReasonPhrase}");
            }

            return null;
        }

        public MetricsResponse<DotNetMetric> GetDotNetMetrics(MetricGetRequest<DotNetMetric> request)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{_baseUrl}/api/metrics/dotnet/getbyinterval/from/{request.FromTime}/to/{request.ToTime}");

            requestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");

            var responseMessage = _httpClient.SendAsync(requestMessage).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <MetricsResponse<DotNetMetric>>(responseStream).Result;
                return metricsResponse;
            }
            else
            {
                _logger.LogError($"Response error: {responseMessage.ReasonPhrase}");
            }

            return null;
        }

        public MetricsResponse<RamMetric> GetRamMetrics(MetricGetRequest<RamMetric> request)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{_baseUrl}/api/metrics/ram/getbyinterval/from/{request.FromTime}/to/{request.ToTime}");

            requestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");

            var responseMessage = _httpClient.SendAsync(requestMessage).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <MetricsResponse<RamMetric>>(responseStream).Result;
                return metricsResponse;
            }
            else
            {
                _logger.LogError($"Response error: {responseMessage.ReasonPhrase}");
            }

            return null;
        }
        
        public MetricsResponse<NetworkMetric> GetNetworkMetrics(MetricGetRequest<NetworkMetric> request)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{_baseUrl}/api/metrics/network/getbyinterval/from/{request.FromTime}/to/{request.ToTime}");

            requestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");

            var responseMessage = _httpClient.SendAsync(requestMessage).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var metricsResponse = JsonSerializer.DeserializeAsync
                    <MetricsResponse<NetworkMetric>>(responseStream).Result;
                return metricsResponse;
            }
            else
            {
                _logger.LogError($"Response error: {responseMessage.ReasonPhrase}");
            }

            return null;
        }
    }
}