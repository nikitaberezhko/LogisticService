using CommonModel.Contracts;
using HubService.Contracts.Request;
using HubService.Contracts.Response;
using Refit;

namespace Infrastructure.Refit.Clients;

public interface IHubApi
{
    [Get("/api/v1/hubs/{request.Id}")]
    Task<CommonResponse<GetHubByIdResponse>> GetHubById(GetHubByIdRequest request);
}