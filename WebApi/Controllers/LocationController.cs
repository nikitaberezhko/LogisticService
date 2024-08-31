using Asp.Versioning;
using AutoMapper;
using CommonModel.Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Request;
using Services.Services.Interfaces;
using WebApi.Models.OtherModels;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/locations")]
[ApiVersion(1)]
public class LocationController(
    IMapper mapper,
    ILocationService locationService)
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommonResponse<GetLocationResponse>>> GetLocation(
        [FromRoute] GetLocationRequest request)
    {
        var result = await locationService
            .GetLocation(mapper.Map<GetLocationModel>(request));
        var response = new CommonResponse<GetLocationResponse>
            { Data = mapper.Map<GetLocationResponse>(result) };

        return response;
    }

    [HttpGet]
    public async Task<ActionResult<CommonResponse<GetContainersLocationResponse>>>
        GetContainersLocation(GetContainersLocationRequest request)
    {
        var result = await locationService
            .GetContainersLocation(mapper.Map<GetContainersLocationModel>(request));
        var response = new CommonResponse<GetContainersLocationResponse>
        {
            Data = new GetContainersLocationResponse 
                { Containers = mapper.Map<List<ContainerApiModel>>(result)}
        };
        
        return response;
    }

    [HttpGet("orders/{id:guid}")]
    public async Task<ActionResult<CommonResponse<GetContainersLocationByOrderIdResponse>>>
        GetContainersLocationByOrderId([FromRoute]GetContainersLocationByOrderIdRequest request)
    {
        var result = await locationService
            .GetContainersLocationByOrderId(
                mapper.Map<GetContainersLocationByOrderIdModel>(request));
        var response = new CommonResponse<GetContainersLocationByOrderIdResponse>
        {
            Data = new GetContainersLocationByOrderIdResponse
            { Containers = mapper.Map<List<ContainerApiModel>>(result) }
        };
        
        return response;
    }
    
    // todo: check
    [HttpPut("{request.Id:guid}")]
    public async Task<ActionResult<CommonResponse<UpdateLocationResponse>>>
        UpdateLocation(UpdateLocationRequest request)
    {
        var result = await locationService
            .UpdateLocation(mapper.Map<UpdateLocationModel>(request));
        var response = new CommonResponse<UpdateLocationResponse> 
            { Data = mapper.Map<UpdateLocationResponse>(result) };
        
        return response;
    }
    
    [HttpPut]
    public async Task<ActionResult<CommonResponse<UpdateContainersLocationResponse>>>
        UpdateContainersLocation(UpdateContainersLocationRequest request)
    {
        var result = await locationService
            .UpdateContainersLocation(mapper.Map<UpdateContainersLocationModel>(request));
        var response = new CommonResponse<UpdateContainersLocationResponse>
        {
            Data = new UpdateContainersLocationResponse 
                { Containers = mapper.Map<List<ContainerApiModel>>(result) }
        };
        
        return response;
    }
}