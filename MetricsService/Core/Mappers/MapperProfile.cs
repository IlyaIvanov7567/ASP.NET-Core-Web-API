using AutoMapper;
using Core.DAL.Models;
using Core.DAL.Responses;

namespace Core.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        { 
            CreateMap<CpuMetric, MetricDto<CpuMetric>>();
            CreateMap<DotNetMetric, MetricDto<DotNetMetric>>();
            CreateMap<HddMetric, MetricDto<HddMetric>>();
            CreateMap<NetworkMetric, MetricDto<NetworkMetric>>();
            CreateMap<RamMetric, MetricDto<RamMetric>>();
        }
    }
}
