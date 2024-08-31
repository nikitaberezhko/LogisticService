using AutoMapper;
using Services.Models.Request;
using Services.Models.Response;
using WebApi.Models.OtherModels;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // Requests -> Request models
        CreateMap<GetLocationRequest, GetLocationModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        CreateMap<GetContainersLocationRequest, GetContainersLocationModel>()
            .ForMember(d => d.IdsList, map => map.MapFrom(c => c.IdsList));
        
        
        CreateMap<GetContainersLocationByOrderIdRequest, GetContainersLocationByOrderIdModel>()
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId));
        
        
        CreateMap<UpdateLocationRequest, UpdateLocationModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude));


        CreateMap<UpdateLocationApiModel, UpdateLocationModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude));
        
        

        // Response models -> Responses
        CreateMap<ContainerModel, GetLocationResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.MapFrom(c => c.LastUpdateTime));
        
        
        CreateMap<ContainerModel, UpdateLocationResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.MapFrom(c => c.LastUpdateTime));
        
        
        CreateMap<ContainerModel, ContainerApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.MapFrom(c => c.LastUpdateTime));
    }
}