using AutoMapper;
using Domain;
using Services.Models.Request;
using Services.Models.Response;

namespace Services.Mapper;

public class ServiceMappingProfile : Profile
{
    public ServiceMappingProfile()
    {
        // Request models -> Domain models
        CreateMap<GetLocationModel, Container>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.Ignore())
            .ForMember(d => d.Latitude, map => map.Ignore())
            .ForMember(d => d.Longitude, map => map.Ignore())
            .ForMember(d => d.LastUpdateTime, map => map.Ignore());

        
        CreateMap<UpdateLocationModel, Container>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.Ignore())
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.Ignore());
        
        
        CreateMap<UpdateLocationModel, Container>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.Ignore())
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.Ignore());
        
        
        
        // Domain models -> Response models
        CreateMap<Container, ContainerModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.MapFrom(c => c.LastUpdateTime));;
    }
}