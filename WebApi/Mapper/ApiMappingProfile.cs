using AutoMapper;
using LogisticService.Contracts.OtherModels;
using LogisticService.Contracts.Request;
using LogisticService.Contracts.Response;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Models.Response;

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


        CreateMap<UpdateLocationApiModel, ContainerUpdateModel>()
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
        
        
        CreateMap<ContainerModel, LogisticContainerApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.MapFrom(c => c.OrderId))
            .ForMember(d => d.Latitude, map => map.MapFrom(c => c.Latitude))
            .ForMember(d => d.Longitude, map => map.MapFrom(c => c.Longitude))
            .ForMember(d => d.LastUpdateTime, map => map.MapFrom(c => c.LastUpdateTime));
    }
}