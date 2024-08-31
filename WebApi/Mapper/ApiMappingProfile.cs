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
        CreateMap<GetContainerLocationRequest, GetLocationModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        CreateMap<GetContainersListLocationRequest, GetContainersLocationModel>()
            .ForMember(d => d.IdsList, map => map.MapFrom(c => c.IdsList));
        
        
        CreateMap<GetContainerListByOrderIdRequest, GetContainersByOrderIdModel>()
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId));
        
        
        CreateMap<UpdateContainerLocationRequest, UpdateLocationModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude));


        CreateMap<UpdateLocationApiModel, UpdateLocationModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude));
        
        

        // Response models -> Responses
        CreateMap<ContainerModel, GetContainerLocationResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.MapFrom(c => c.LastUpdateTime));
        
        
        CreateMap<ContainerModel, UpdateContainerLocationResponse>()
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