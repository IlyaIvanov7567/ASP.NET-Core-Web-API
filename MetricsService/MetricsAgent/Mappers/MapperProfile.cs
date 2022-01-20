using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        { 
            CreateMap<CpuMetric, MetricDto<CpuMetric>>().ReverseMap();
            CreateMap<CpuMetric, MetricDto<DotNetMetric>>().ReverseMap();
            CreateMap<CpuMetric, MetricDto<HddMetric>>().ReverseMap();
            CreateMap<CpuMetric, MetricDto<NetworkMetric>>().ReverseMap();
            CreateMap<CpuMetric, MetricDto<RamMetric>>().ReverseMap();
        }
    }
}
