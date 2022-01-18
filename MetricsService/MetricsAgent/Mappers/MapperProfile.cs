using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        { 
            CreateMap<CpuMetric, MetricDto<CpuMetric>>();
            CreateMap<CpuMetric, MetricDto<DotNetMetric>>();
            CreateMap<CpuMetric, MetricDto<HddMetric>>();
            CreateMap<CpuMetric, MetricDto<NetworkMetric>>();
            CreateMap<CpuMetric, MetricDto<RamMetric>>();
        }
    }
}
